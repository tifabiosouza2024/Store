CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;
CREATE TABLE `operations` (
    `id` char(36) NOT NULL,
    `document` varchar(18) NOT NULL,
    `name` varchar(255) NOT NULL,
    `email` varchar(255) NOT NULL,
    `is_active` tinyint(1) NULL DEFAULT TRUE,
    `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `deleted` tinyint(1) NOT NULL DEFAULT FALSE,
    PRIMARY KEY (`id`)
);

CREATE TABLE `roles` (
    `id` int NOT NULL AUTO_INCREMENT,
    `name` varchar(255) NOT NULL,
    `normalized_name` varchar(255) NOT NULL,
    `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `deleted` tinyint(1) NOT NULL DEFAULT FALSE,
    PRIMARY KEY (`id`)
);

CREATE TABLE `users` (
    `id` char(36) NOT NULL,
    `email` varchar(255) NOT NULL,
    `name` varchar(255) NOT NULL,
    `password_hash` varchar(255) NOT NULL,
    `is_active` tinyint(1) NOT NULL DEFAULT FALSE,
    `is_beta` tinyint(1) NOT NULL DEFAULT FALSE,
    `remember_me` tinyint(1) NOT NULL DEFAULT FALSE,
    `last_operation_id` char(36) NULL,
    `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `deleted` tinyint(1) NOT NULL DEFAULT FALSE,
    PRIMARY KEY (`id`),
    CONSTRAINT `FK_users_operations_last_operation_id` FOREIGN KEY (`last_operation_id`) REFERENCES `operations` (`id`)
);

CREATE TABLE `operation_user_role` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `operation_id` char(36) NOT NULL,
    `user_id` char(36) NOT NULL,
    `role_id` int NOT NULL,
    `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `deleted` tinyint(1) NOT NULL DEFAULT FALSE,
    PRIMARY KEY (`id`),
    CONSTRAINT `FK_operation_user_role_operations_operation_id` FOREIGN KEY (`operation_id`) REFERENCES `operations` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_operation_user_role_roles_role_id` FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_operation_user_role_users_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE
);

CREATE INDEX `IX_operation_user_role_operation_id` ON `operation_user_role` (`operation_id`);

CREATE INDEX `IX_operation_user_role_role_id` ON `operation_user_role` (`role_id`);

CREATE UNIQUE INDEX `IX_operation_user_role_user_id_operation_id_role_id` ON `operation_user_role` (`user_id`, `operation_id`, `role_id`);

CREATE UNIQUE INDEX `IX_operations_document` ON `operations` (`document`);

CREATE UNIQUE INDEX `IX_operations_document_email` ON `operations` (`document`, `email`);

CREATE UNIQUE INDEX `IX_operations_email` ON `operations` (`email`);

CREATE UNIQUE INDEX `IX_users_email` ON `users` (`email`);

CREATE INDEX `IX_users_last_operation_id` ON `users` (`last_operation_id`);

INSERT INTO roles (name, normalized_name, deleted) VALUES ('Super', 'SUPER', false);

INSERT INTO roles (name, normalized_name, deleted) VALUES ('Admin', 'ADMIN', false);

INSERT INTO roles (name, normalized_name, deleted) VALUES ('Manager', 'MANAGER', false);

iNSERT INTO roles (name, normalized_name, deleted) VALUES ('Seller', 'SELLER', false);

INSERT INTO users (id, name, email, password_hash, is_active, is_beta) VALUES (uuid(),'Super User', 'superuser@gmail.com', 'AQAAAAIAAYagAAAAEB3cyvVUPk198G/Xke21lsxb5LZ7O+0k16HiJIIMYhGoVtF8jhQ0/S7VOQl+pMZZTw==', true, true);

INSERT INTO users (id, name, email, password_hash, is_active, is_beta) VALUES (uuid(),'Seller User', 'selleruser@gmail.com', 'AQAAAAIAAYagAAAAEB3cyvVUPk198G/Xke21lsxb5LZ7O+0k16HiJIIMYhGoVtF8jhQ0/S7VOQl+pMZZTw==', true, true);

INSERT INTO operations (id, document, name, email) VALUES (uuid(),'40.953.557/0001-61', 'First Gift', 'firstgift@mail.com');

INSERT INTO operations (id, document, name, email) VALUES (uuid(),'67.192.514/0001-67', 'Seccond Gift', 'seccondgift@mail.com');


INSERT INTO operation_user_role (user_id, role_id, operation_id)
SELECT
    (SELECT id FROM users WHERE email like 'superuser%'), r.id, o.id
FROM roles r
CROSS JOIN operations o
WHERE r.normalized_name = 'SUPER';


INSERT INTO operation_user_role (user_id, role_id, operation_id)
SELECT
    (SELECT id FROM users WHERE email like 'selleruser%'), r.id, o.id
FROM roles r
CROSS JOIN operations o
WHERE r.normalized_name = 'SELLER';

INSERT INTO users (id, name, email, password_hash, is_active, is_beta) VALUES (uuid(),'Super Alone', 'superalone@gmail.com', 'AQAAAAIAAYagAAAAEB3cyvVUPk198G/Xke21lsxb5LZ7O+0k16HiJIIMYhGoVtF8jhQ0/S7VOQl+pMZZTw==', true, true);

INSERT INTO operations (id, document, name, email) VALUES (uuid(),'93.176.710/0001-58', 'Third Gift', 'thirdgift@mail.com');


INSERT INTO operation_user_role (user_id, role_id, operation_id)
SELECT
    (SELECT id FROM users WHERE email like 'superalone%'), r.id, o.id
FROM roles r
cross JOIN operations o
WHERE r.normalized_name = 'ADMIN' AND o.email LIKE 'third%';

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20261024014754_CreateUserAndRoleAndOperationAndOperationUserRoles', '10.0.14');

COMMIT;


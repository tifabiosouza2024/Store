CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;
CREATE TABLE `global_roles` (
    `id` int NOT NULL AUTO_INCREMENT,
    `name` varchar(255) NOT NULL,
    `normalized_name` varchar(255) NOT NULL,
    `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `deleted` tinyint(1) NOT NULL DEFAULT FALSE,
    PRIMARY KEY (`id`)
);

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
    `name` varchar(255) NULL,
    `password_hash` varchar(255) NOT NULL,
    `is_active` tinyint(1) NOT NULL DEFAULT TRUE,
    `is_beta` tinyint(1) NOT NULL DEFAULT FALSE,
    `remember_me` tinyint(1) NOT NULL DEFAULT FALSE,
    `must_change_password` tinyint(1) NOT NULL DEFAULT TRUE,
    `last_operation_id` char(36) NULL,
    `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `deleted` tinyint(1) NOT NULL DEFAULT FALSE,
    PRIMARY KEY (`id`),
    CONSTRAINT `FK_users_operations_last_operation_id` FOREIGN KEY (`last_operation_id`) REFERENCES `operations` (`id`)
);

CREATE TABLE `global_user_roles` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `is_active` tinyint(1) NOT NULL DEFAULT TRUE,
    `status` int NOT NULL,
    `invited_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `accepted_at` datetime(0) NULL,
    `user_id` char(36) NOT NULL,
    `role_id` int NOT NULL,
    `invited_by_user_id` char(36) NULL,
    `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `deleted` tinyint(1) NOT NULL DEFAULT FALSE,
    PRIMARY KEY (`id`),
    CONSTRAINT `FK_global_user_roles_global_roles_role_id` FOREIGN KEY (`role_id`) REFERENCES `global_roles` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_global_user_roles_users_invited_by_user_id` FOREIGN KEY (`invited_by_user_id`) REFERENCES `users` (`id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_global_user_roles_users_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE
);

CREATE TABLE `operation_user_role` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `is_active` tinyint(1) NOT NULL DEFAULT TRUE,
    `status` int NOT NULL,
    `invited_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `accepted_at` datetime(0) NULL,
    `operation_id` char(36) NOT NULL,
    `user_id` char(36) NOT NULL,
    `role_id` int NOT NULL,
    `invited_by_user_id` char(36) NULL,
    `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `deleted` tinyint(1) NOT NULL DEFAULT FALSE,
    PRIMARY KEY (`id`),
    CONSTRAINT `FK_operation_user_role_operations_operation_id` FOREIGN KEY (`operation_id`) REFERENCES `operations` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_operation_user_role_roles_role_id` FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_operation_user_role_users_invited_by_user_id` FOREIGN KEY (`invited_by_user_id`) REFERENCES `users` (`id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_operation_user_role_users_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE
);

CREATE INDEX `IX_global_user_roles_invited_by_user_id` ON `global_user_roles` (`invited_by_user_id`);

CREATE INDEX `IX_global_user_roles_role_id` ON `global_user_roles` (`role_id`);

CREATE UNIQUE INDEX `IX_global_user_roles_user_id_role_id` ON `global_user_roles` (`user_id`, `role_id`);

CREATE INDEX `IX_operation_user_role_invited_by_user_id` ON `operation_user_role` (`invited_by_user_id`);

CREATE INDEX `IX_operation_user_role_operation_id` ON `operation_user_role` (`operation_id`);

CREATE INDEX `IX_operation_user_role_role_id` ON `operation_user_role` (`role_id`);

CREATE UNIQUE INDEX `IX_operation_user_role_user_id_operation_id_role_id` ON `operation_user_role` (`user_id`, `operation_id`, `role_id`);

CREATE UNIQUE INDEX `IX_operations_document` ON `operations` (`document`);

CREATE UNIQUE INDEX `IX_operations_document_email` ON `operations` (`document`, `email`);

CREATE UNIQUE INDEX `IX_operations_email` ON `operations` (`email`);

CREATE UNIQUE INDEX `IX_users_email` ON `users` (`email`);

CREATE INDEX `IX_users_last_operation_id` ON `users` (`last_operation_id`);


INSERT INTO global_roles (name, normalized_name, deleted) VALUES
('Super', 'SUPER', false);


INSERT INTO roles (name, normalized_name, deleted) VALUES
('Admin', 'ADMIN', false),
('Manager', 'MANAGER', false),
('Seller', 'SELLER', false);


INSERT INTO users (id, name, email, password_hash, is_active, is_beta) VALUES
(uuid(),'Super User', 'superuser@mail.com', 'AQAAAAIAAYagAAAAEMZ46j7ubcp8spuFbSaxdj/Rs5KPwjEUQmgq1b0UuTdAcVXT+SswSckURMWaNVf/Pw==', true, true);


INSERT INTO global_user_roles (user_id, role_id, is_active, status)
SELECT
    (SELECT id FROM users WHERE email = 'superuser@mail.com'), r.id, true, 2
FROM global_roles r
WHERE r.normalized_name = 'SUPER';

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20261117213839_CreateAccessManagement', '10.0.14');

COMMIT;


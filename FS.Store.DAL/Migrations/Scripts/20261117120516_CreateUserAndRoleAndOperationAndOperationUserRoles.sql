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

CREATE TABLE `payment_provider` (
    `id` int NOT NULL AUTO_INCREMENT,
    `name` varchar(50) NOT NULL,
    `logo_icon` varchar(255) NULL,
    `created_at` datetime(0) NOT NULL,
    `updated_at` datetime(0) NULL,
    `deleted` tinyint(1) NOT NULL,
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

CREATE TABLE `payment_terminal` (
    `id` char(36) NOT NULL,
    `name` varchar(50) NOT NULL,
    `payment_provider_id` int NOT NULL,
    `operation_id` char(36) NOT NULL,
    `created_at` datetime(0) NOT NULL,
    `updated_at` datetime(0) NULL,
    `deleted` tinyint(1) NOT NULL,
    PRIMARY KEY (`id`),
    CONSTRAINT `FK_payment_terminal_operations_operation_id` FOREIGN KEY (`operation_id`) REFERENCES `operations` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_payment_terminal_payment_provider_payment_provider_id` FOREIGN KEY (`payment_provider_id`) REFERENCES `payment_provider` (`id`) ON DELETE CASCADE
);

CREATE TABLE `cash_register_session` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `opened_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `closed_at` datetime(0) NULL,
    `opening_amount` decimal(10,2) NOT NULL,
    `total_sales` decimal(10,2) NOT NULL,
    `cash_register_status` int NOT NULL,
    `cash_session_number` int NOT NULL,
    `operation_id` char(36) NOT NULL,
    `opened_by_user_id` char(36) NOT NULL,
    `closed_by_user_id` char(36) NULL,
    `changed_by_user_id` char(36) NULL,
    `created_at` datetime(0) NOT NULL,
    `updated_at` datetime(0) NULL,
    `deleted` tinyint(1) NOT NULL,
    PRIMARY KEY (`id`),
    CONSTRAINT `FK_cash_register_session_operations_operation_id` FOREIGN KEY (`operation_id`) REFERENCES `operations` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_cash_register_session_users_changed_by_user_id` FOREIGN KEY (`changed_by_user_id`) REFERENCES `users` (`id`),
    CONSTRAINT `FK_cash_register_session_users_closed_by_user_id` FOREIGN KEY (`closed_by_user_id`) REFERENCES `users` (`id`),
    CONSTRAINT `FK_cash_register_session_users_opened_by_user_id` FOREIGN KEY (`opened_by_user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE
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

CREATE TABLE `cash_closing` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `closed_at` datetime(0) NOT NULL,
    `cash_closing_amount` decimal(10,2) NOT NULL,
    `total_credit` decimal(10,2) NOT NULL,
    `total_debit` decimal(10,2) NOT NULL,
    `total_pix` decimal(10,2) NOT NULL,
    `total_expenses` decimal(10,2) NOT NULL,
    `total_withdrawal` decimal(10,2) NOT NULL,
    `total_cash_inflows` decimal(10,2) NOT NULL,
    `notes` varchar(255) NULL,
    `cash_register_session_id` bigint NOT NULL,
    `closed_by_user_id` char(36) NOT NULL,
    `created_at` datetime(0) NOT NULL,
    `updated_at` datetime(0) NULL,
    `deleted` tinyint(1) NOT NULL,
    PRIMARY KEY (`id`),
    CONSTRAINT `FK_cash_closing_cash_register_session_cash_register_session_id` FOREIGN KEY (`cash_register_session_id`) REFERENCES `cash_register_session` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_cash_closing_users_closed_by_user_id` FOREIGN KEY (`closed_by_user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE
);

CREATE TABLE `cash_movement` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `cash_movement_type` int NOT NULL,
    `amount` decimal(10,2) NOT NULL,
    `description` varchar(255) NULL,
    `occurred_at` datetime(0) NOT NULL,
    `canceled_at` datetime(0) NULL,
    `cancellation_reason` varchar(255) NULL,
    `created_by_user_id` char(36) NOT NULL,
    `canceled_by_user_id` char(36) NULL,
    `cash_register_session_id` bigint NOT NULL,
    `created_at` datetime(0) NOT NULL,
    `updated_at` datetime(0) NULL,
    `deleted` tinyint(1) NOT NULL,
    PRIMARY KEY (`id`),
    CONSTRAINT `FK_cash_movement_cash_register_session_cash_register_session_id` FOREIGN KEY (`cash_register_session_id`) REFERENCES `cash_register_session` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_cash_movement_users_canceled_by_user_id` FOREIGN KEY (`canceled_by_user_id`) REFERENCES `users` (`id`),
    CONSTRAINT `FK_cash_movement_users_created_by_user_id` FOREIGN KEY (`created_by_user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE
);

CREATE TABLE `cash_closing_terminal` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `closed_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP,
    `credit_amount` decimal(10,2) NOT NULL,
    `debit_amount` decimal(10,2) NOT NULL,
    `pix_amount` decimal(10,2) NOT NULL,
    `payment_terminal_id` char(36) NOT NULL,
    `cash_register_session_id` bigint NOT NULL,
    `cash_closing_id` bigint NOT NULL,
    `created_by_user_id` char(36) NOT NULL,
    `created_at` datetime(0) NOT NULL,
    `updated_at` datetime(0) NULL,
    `deleted` tinyint(1) NOT NULL,
    PRIMARY KEY (`id`),
    CONSTRAINT `FK_cash_closing_terminal_cash_closing_cash_closing_id` FOREIGN KEY (`cash_closing_id`) REFERENCES `cash_closing` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_cash_closing_terminal_cash_register_session_cash_register_se~` FOREIGN KEY (`cash_register_session_id`) REFERENCES `cash_register_session` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_cash_closing_terminal_payment_terminal_payment_terminal_id` FOREIGN KEY (`payment_terminal_id`) REFERENCES `payment_terminal` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_cash_closing_terminal_users_created_by_user_id` FOREIGN KEY (`created_by_user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE
);

CREATE UNIQUE INDEX `IX_cash_closing_cash_register_session_id` ON `cash_closing` (`cash_register_session_id`);

CREATE INDEX `IX_cash_closing_closed_by_user_id` ON `cash_closing` (`closed_by_user_id`);

CREATE INDEX `IX_cash_closing_terminal_cash_closing_id` ON `cash_closing_terminal` (`cash_closing_id`);

CREATE INDEX `IX_cash_closing_terminal_cash_register_session_id` ON `cash_closing_terminal` (`cash_register_session_id`);

CREATE INDEX `IX_cash_closing_terminal_created_by_user_id` ON `cash_closing_terminal` (`created_by_user_id`);

CREATE INDEX `IX_cash_closing_terminal_payment_terminal_id` ON `cash_closing_terminal` (`payment_terminal_id`);

CREATE INDEX `IX_cash_movement_canceled_by_user_id` ON `cash_movement` (`canceled_by_user_id`);

CREATE INDEX `IX_cash_movement_cash_register_session_id` ON `cash_movement` (`cash_register_session_id`);

CREATE INDEX `IX_cash_movement_created_by_user_id` ON `cash_movement` (`created_by_user_id`);

CREATE INDEX `IX_cash_register_session_changed_by_user_id` ON `cash_register_session` (`changed_by_user_id`);

CREATE INDEX `IX_cash_register_session_closed_by_user_id` ON `cash_register_session` (`closed_by_user_id`);

CREATE INDEX `IX_cash_register_session_opened_by_user_id` ON `cash_register_session` (`opened_by_user_id`);

CREATE INDEX `IX_cash_register_session_operation_id` ON `cash_register_session` (`operation_id`);

CREATE INDEX `IX_operation_user_role_invited_by_user_id` ON `operation_user_role` (`invited_by_user_id`);

CREATE INDEX `IX_operation_user_role_operation_id` ON `operation_user_role` (`operation_id`);

CREATE INDEX `IX_operation_user_role_role_id` ON `operation_user_role` (`role_id`);

CREATE UNIQUE INDEX `IX_operation_user_role_user_id_operation_id_role_id` ON `operation_user_role` (`user_id`, `operation_id`, `role_id`);

CREATE UNIQUE INDEX `IX_operations_document` ON `operations` (`document`);

CREATE UNIQUE INDEX `IX_operations_document_email` ON `operations` (`document`, `email`);

CREATE UNIQUE INDEX `IX_operations_email` ON `operations` (`email`);

CREATE INDEX `IX_payment_terminal_operation_id` ON `payment_terminal` (`operation_id`);

CREATE INDEX `IX_payment_terminal_payment_provider_id` ON `payment_terminal` (`payment_provider_id`);

CREATE UNIQUE INDEX `IX_users_email` ON `users` (`email`);

CREATE INDEX `IX_users_last_operation_id` ON `users` (`last_operation_id`);


INSERT INTO roles (name, normalized_name, deleted) VALUES
('Super', 'SUPER', false),
('Admin', 'ADMIN', false),
('Manager', 'MANAGER', false),
('Seller', 'SELLER', false);


INSERT INTO users (id, name, email, password_hash, is_active, is_beta) VALUES
(uuid(),'Super User', 'superuser@gmail.com', 'AQAAAAIAAYagAAAAEEMxJwmG0mt2lLMideICfyRWErAc/YTabcWxisnqK+LgWepq5UCX836/B+OZN0jNgg==', true, true);

INSERT INTO operations (id, document, name, email) VALUES (uuid(),'40.953.557/0001-61', 'First Gift', 'firstgift@mail.com');


INSERT INTO operation_user_role (user_id, role_id, operation_id, is_active, status)
SELECT
    (SELECT id FROM users WHERE email LIKE 'superuser%'), r.id, o.id, true, 1
FROM roles r
CROSS JOIN operations o
WHERE r.normalized_name = 'SUPER';

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20261117120516_CreateUserAndRoleAndOperationAndOperationUserRoles', '10.0.14');

COMMIT;


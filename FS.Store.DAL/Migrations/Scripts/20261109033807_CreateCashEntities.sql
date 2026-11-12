START TRANSACTION;
CREATE TABLE `cash_register_session` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `opened_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `closed_at` datetime(0) NULL,
    `opening_amount` decimal(10,2) NOT NULL,
    `cash_register_status` int NOT NULL,
    `cash_session_number` int NOT NULL,
    `operation_id` char(36) NOT NULL,
    `opened_by_user_id` char(36) NOT NULL,
    `closed_by_user_id` char(36) NULL,
    `changed_by_user_id` char(36) NULL,
    `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `deleted` tinyint(1) NOT NULL DEFAULT FALSE,
    PRIMARY KEY (`id`),
    CONSTRAINT `FK_cash_register_session_operations_operation_id` FOREIGN KEY (`operation_id`) REFERENCES `operations` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_cash_register_session_users_changed_by_user_id` FOREIGN KEY (`changed_by_user_id`) REFERENCES `users` (`id`),
    CONSTRAINT `FK_cash_register_session_users_closed_by_user_id` FOREIGN KEY (`closed_by_user_id`) REFERENCES `users` (`id`),
    CONSTRAINT `FK_cash_register_session_users_opened_by_user_id` FOREIGN KEY (`opened_by_user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE
);

CREATE TABLE `payment_provider` (
    `id` int NOT NULL AUTO_INCREMENT,
    `name` varchar(50) NOT NULL,
    `logo_icon` varchar(255) NULL,
    `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `deleted` tinyint(1) NOT NULL DEFAULT FALSE,
    PRIMARY KEY (`id`)
);

CREATE TABLE `cash_closing` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `closed_at` datetime(0) NOT NULL,
    `cash_amount` decimal(18,2) NOT NULL,
    `total_credit` decimal(18,2) NOT NULL,
    `total_debit` decimal(18,2) NOT NULL,
    `total_pix` decimal(18,2) NOT NULL,
    `notes` varchar(255) NULL,
    `cash_register_session_id` bigint NOT NULL,
    `closed_by_user_id` char(36) NOT NULL,
    `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `deleted` tinyint(1) NOT NULL DEFAULT FALSE,
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
    `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `deleted` tinyint(1) NOT NULL DEFAULT FALSE,
    PRIMARY KEY (`id`),
    CONSTRAINT `FK_cash_movement_cash_register_session_cash_register_session_id` FOREIGN KEY (`cash_register_session_id`) REFERENCES `cash_register_session` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_cash_movement_users_canceled_by_user_id` FOREIGN KEY (`canceled_by_user_id`) REFERENCES `users` (`id`),
    CONSTRAINT `FK_cash_movement_users_created_by_user_id` FOREIGN KEY (`created_by_user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE
);

CREATE TABLE `payment_terminal` (
    `id` char(36) NOT NULL,
    `name` varchar(50) NOT NULL,
    `payment_provider_id` int NOT NULL,
    `operation_id` char(36) NOT NULL,
    `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `deleted` tinyint(1) NOT NULL DEFAULT FALSE,
    PRIMARY KEY (`id`),
    CONSTRAINT `FK_payment_terminal_operations_operation_id` FOREIGN KEY (`operation_id`) REFERENCES `operations` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_payment_terminal_payment_provider_payment_provider_id` FOREIGN KEY (`payment_provider_id`) REFERENCES `payment_provider` (`id`) ON DELETE CASCADE
);

CREATE TABLE `cash_closing_terminal` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `credit_amount` decimal(10,2) NOT NULL,
    `debit_amount` decimal(10,2) NOT NULL,
    `pix_amount` decimal(10,2) NOT NULL,
    `payment_terminal_id` char(36) NOT NULL,
    `cash_register_session_id` bigint NOT NULL,
    `created_by_user_id` char(36) NOT NULL,
    `CashClosingId` bigint NULL,
    `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `deleted` tinyint(1) NOT NULL DEFAULT FALSE,
    PRIMARY KEY (`id`),
    CONSTRAINT `FK_cash_closing_terminal_cash_closing_CashClosingId` FOREIGN KEY (`CashClosingId`) REFERENCES `cash_closing` (`id`),
    CONSTRAINT `FK_cash_closing_terminal_cash_register_session_cash_register_se~` FOREIGN KEY (`cash_register_session_id`) REFERENCES `cash_register_session` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_cash_closing_terminal_payment_terminal_payment_terminal_id` FOREIGN KEY (`payment_terminal_id`) REFERENCES `payment_terminal` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_cash_closing_terminal_users_created_by_user_id` FOREIGN KEY (`created_by_user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE
);

CREATE INDEX `IX_cash_closing_cash_register_session_id` ON `cash_closing` (`cash_register_session_id`);

CREATE INDEX `IX_cash_closing_closed_by_user_id` ON `cash_closing` (`closed_by_user_id`);

CREATE INDEX `IX_cash_closing_terminal_cash_register_session_id` ON `cash_closing_terminal` (`cash_register_session_id`);

CREATE INDEX `IX_cash_closing_terminal_CashClosingId` ON `cash_closing_terminal` (`CashClosingId`);

CREATE INDEX `IX_cash_closing_terminal_created_by_user_id` ON `cash_closing_terminal` (`created_by_user_id`);

CREATE INDEX `IX_cash_closing_terminal_payment_terminal_id` ON `cash_closing_terminal` (`payment_terminal_id`);

CREATE INDEX `IX_cash_movement_canceled_by_user_id` ON `cash_movement` (`canceled_by_user_id`);

CREATE INDEX `IX_cash_movement_cash_register_session_id` ON `cash_movement` (`cash_register_session_id`);

CREATE INDEX `IX_cash_movement_created_by_user_id` ON `cash_movement` (`created_by_user_id`);

CREATE INDEX `IX_cash_register_session_changed_by_user_id` ON `cash_register_session` (`changed_by_user_id`);

CREATE INDEX `IX_cash_register_session_closed_by_user_id` ON `cash_register_session` (`closed_by_user_id`);

CREATE INDEX `IX_cash_register_session_opened_by_user_id` ON `cash_register_session` (`opened_by_user_id`);

CREATE INDEX `IX_cash_register_session_operation_id` ON `cash_register_session` (`operation_id`);

CREATE INDEX `IX_payment_terminal_operation_id` ON `payment_terminal` (`operation_id`);

CREATE INDEX `IX_payment_terminal_payment_provider_id` ON `payment_terminal` (`payment_provider_id`);


INSERT INTO payment_provider (name, logo_icon) values
('InfinitePay', 'bank-icons/infinitepay.webp'), ('Stone', 'bank-icons/stone.png');

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20261109033807_CreateCashEntities', '10.0.14');

COMMIT;


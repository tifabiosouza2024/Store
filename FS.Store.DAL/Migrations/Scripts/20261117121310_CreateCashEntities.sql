START TRANSACTION;
ALTER TABLE `payment_terminal` MODIFY `updated_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;

ALTER TABLE `payment_terminal` MODIFY `deleted` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `payment_terminal` MODIFY `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP;

ALTER TABLE `payment_provider` MODIFY `updated_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;

ALTER TABLE `payment_provider` MODIFY `deleted` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `payment_provider` MODIFY `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP;

ALTER TABLE `cash_register_session` MODIFY `updated_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;

ALTER TABLE `cash_register_session` MODIFY `deleted` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `cash_register_session` MODIFY `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP;

ALTER TABLE `cash_movement` MODIFY `updated_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;

ALTER TABLE `cash_movement` MODIFY `deleted` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `cash_movement` MODIFY `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP;

ALTER TABLE `cash_closing_terminal` MODIFY `updated_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;

ALTER TABLE `cash_closing_terminal` MODIFY `deleted` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `cash_closing_terminal` MODIFY `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP;

ALTER TABLE `cash_closing` MODIFY `updated_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;

ALTER TABLE `cash_closing` MODIFY `deleted` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `cash_closing` MODIFY `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP;


INSERT INTO payment_provider (name, logo_icon) values
('InfinitePay', 'bank-icons/infinitepay.webp'), ('Stone', 'bank-icons/stone.png');

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20261117121310_CreateCashEntities', '10.0.14');

COMMIT;


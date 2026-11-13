START TRANSACTION;
ALTER TABLE `cash_closing_terminal` DROP CONSTRAINT `FK_cash_closing_terminal_cash_closing_CashClosingId`;

ALTER TABLE `cash_closing_terminal` CHANGE `CashClosingId` `cash_closing_id` bigint NOT NULL DEFAULT 0;

ALTER TABLE `cash_closing_terminal` RENAME INDEX `IX_cash_closing_terminal_CashClosingId` TO `IX_cash_closing_terminal_cash_closing_id`;

ALTER TABLE `cash_closing_terminal` MODIFY `cash_closing_id` bigint NOT NULL DEFAULT 0;

ALTER TABLE `cash_closing_terminal` ADD `closed_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP;

ALTER TABLE `cash_closing_terminal` ADD CONSTRAINT `FK_cash_closing_terminal_cash_closing_cash_closing_id` FOREIGN KEY (`cash_closing_id`) REFERENCES `cash_closing` (`id`) ON DELETE CASCADE;


UPDATE cash_closing_terminal set cash_closing_id = (SELECT id FROM cash_closing WHERE cash_register_session_id = cash_closing_terminal.cash_register_session_id LIMIT 1) 
WHERE cash_closing_id IS NULL;


INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20261112210634_ChangeCashEntities', '10.0.14');

COMMIT;


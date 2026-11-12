START TRANSACTION;
ALTER TABLE `users` MODIFY `remember_me` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `users` MODIFY `password_hash` varchar(255) NOT NULL;

ALTER TABLE `users` MODIFY `name` varchar(255) NOT NULL;

ALTER TABLE `users` MODIFY `last_operation_id` char(36) NULL;

ALTER TABLE `users` MODIFY `is_beta` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `users` MODIFY `is_active` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `users` MODIFY `email` varchar(255) NOT NULL;

ALTER TABLE `operations` MODIFY `name` varchar(255) NOT NULL;

ALTER TABLE `operations` MODIFY `is_active` tinyint(1) NULL DEFAULT TRUE;

ALTER TABLE `operations` MODIFY `email` varchar(255) NOT NULL;

ALTER TABLE `operations` MODIFY `document` varchar(18) NOT NULL;

ALTER TABLE `operation_user_role` MODIFY `user_id` char(36) NOT NULL;

ALTER TABLE `operation_user_role` MODIFY `role_id` int NOT NULL;

ALTER TABLE `operation_user_role` MODIFY `operation_id` char(36) NOT NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20261026001912_ChangeComments', '10.0.14');

COMMIT;


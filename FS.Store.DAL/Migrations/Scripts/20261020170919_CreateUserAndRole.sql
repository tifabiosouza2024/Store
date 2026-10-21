CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;
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
    `password` varchar(255) NOT NULL,
    `is_active` tinyint(1) NOT NULL,
    `is_beta` tinyint(1) NOT NULL,
    `refresh_token` varchar(255) NULL,
    `dt_expiration_refresh_token` datetime NULL,
    `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `deleted` tinyint(1) NOT NULL DEFAULT FALSE,
    PRIMARY KEY (`id`)
);

CREATE TABLE `user_roles` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `user_id` char(36) NOT NULL,
    `role_id` int NOT NULL,
    `created_at` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `deleted` tinyint(1) NOT NULL DEFAULT FALSE,
    PRIMARY KEY (`id`),
    CONSTRAINT `FK_user_roles_roles_role_id` FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_user_roles_users_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE
);

CREATE INDEX `IX_user_roles_role_id` ON `user_roles` (`role_id`);

CREATE INDEX `IX_user_roles_user_id` ON `user_roles` (`user_id`);

insert into roles (name, normalized_name, deleted) values ('Super', 'SUPER', false);

insert into roles (name, normalized_name, deleted) values ('Seller', 'SELLER', false);

insert into users (id, name, email, password, is_active, is_beta) values (uuid(),'Fabio', 'devfabiosouza@gmail.com', 'AQAAAAIAAYagAAAAEMuGbuS/BNOvx5GiTwDEss+pR07U0l+bRtZR88syFnq6O4OsqaJm4JXi/TNGA7vgiQ==', true, true);

insert into user_roles (user_id, role_id)
select 
	(select id from users limit 1) as id,
	(select id from roles where normalized_name = 'SUPER');

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20261020170919_CreateUserAndRole', '10.0.12');

COMMIT;


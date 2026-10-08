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
    `RolesId` int NOT NULL,
    `UsersId` char(36) NOT NULL,
    PRIMARY KEY (`RolesId`, `UsersId`),
    CONSTRAINT `FK_user_roles_roles_RolesId` FOREIGN KEY (`RolesId`) REFERENCES `roles` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_user_roles_users_UsersId` FOREIGN KEY (`UsersId`) REFERENCES `users` (`id`) ON DELETE CASCADE
);

CREATE INDEX `IX_user_roles_UsersId` ON `user_roles` (`UsersId`);

insert into roles (name, normalized_name, deleted) values ('Super', 'SUPER', false);

insert into roles (name, normalized_name, deleted) values ('Seller', 'SELLER', false);

insert into users (id, email, password, is_active, is_beta) values (uuid(), 'devfabiosouza@gmail.com', 'AQAAAAIAAYagAAAAELu9QuCOulkKeo3yr/a4PxEmjoZBkm1/rLICOZeC8wlnHo31Dn/S4YqokAzMwjueMg==', true, true);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20260918013906_CreateUserAndRole', '10.0.12');

COMMIT;


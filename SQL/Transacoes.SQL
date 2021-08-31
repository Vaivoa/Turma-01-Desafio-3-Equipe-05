CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE SCHEMA IF NOT EXISTS modalmais;

CREATE TABLE modalmais."Transacoes" (
    "Id" uuid NOT NULL,
    "StatusTransacao" integer NOT NULL,
    "Tipo" integer NOT NULL,
    "Chave" character varying(32) NOT NULL,
    "Valor" numeric(8,2) NOT NULL,
    "Descricao" character varying(30) NULL,
    "DataCriacao" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_Transacoes" PRIMARY KEY ("Id"),
    CONSTRAINT "CK_Transacoes_StatusTransacao_Enum" CHECK ("StatusTransacao" IN (0, 1)),
    CONSTRAINT "CK_Transacoes_Valor" CHECK ("Valor" >= 0.01 AND "Valor" <= 5000.00),
    CONSTRAINT "CK_Transacoes_Tipo_Enum" CHECK ("Tipo" IN (1, 2, 3, 4))
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210831151456_teste', '5.0.9');

COMMIT;


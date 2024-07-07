CREATE
EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE "payments"
(
    "Id"        UUID            PRIMARY KEY UNIQUE DEFAULT uuid_generate_v4(),
    "OrderId"   UUID            NOT NULL,
    "Amount"    DECIMAL         NOT NULL,
    "Status"    SMALLINT        NOT NULL,
    "QrCode"    VARCHAR(1000)   NOT NULL,
    "CreatedAt" TIMESTAMPTZ     NOT NULL    DEFAULT NOW(),
    "UpdatedAt" TIMESTAMPTZ     NOT NULL    DEFAULT NOW()
);
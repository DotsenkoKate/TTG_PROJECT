BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "SavedWays" (
	"way_number"	INTEGER NOT NULL,
	"use_count"	INTEGER NOT NULL DEFAULT 1,
	PRIMARY KEY("way_number")
);
CREATE TABLE IF NOT EXISTS "StandartMessage" (
	"id"	INTEGER NOT NULL,
	"text"	TEXT(250) NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Users" (
	"id"	INTEGER NOT NULL,
	"login"	TEXT(40) NOT NULL UNIQUE,
	"password"	TEXT(50) NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "users_standart_message" (
	"id"	INTEGER NOT NULL,
	"user_id"	INTEGER NOT NULL,
	"standart_message_id"	INTEGER NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT),
	FOREIGN KEY("user_id") REFERENCES "Users"("id")
);
CREATE TABLE IF NOT EXISTS "users_ways" (
	"id"	INTEGER NOT NULL,
	"user_id"	INTEGER NOT NULL,
	"way_number"	INTEGER NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT),
	FOREIGN KEY("user_id") REFERENCES "Users"("id")
);
COMMIT;

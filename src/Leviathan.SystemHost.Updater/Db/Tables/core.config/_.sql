-- instances."instance" definition

-- Drop table

-- DROP TABLE instances."instance";

CREATE TABLE core."config" (
	id int4 NOT NULL GENERATED ALWAYS AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE),
	"name" varchar NOT NULL,
	config_json jsonb NULL,
	CONSTRAINT config_pk PRIMARY KEY (id)
);
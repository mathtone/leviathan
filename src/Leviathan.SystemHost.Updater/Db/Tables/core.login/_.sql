-- auth.login definition

-- Drop table

-- DROP TABLE auth.login;

CREATE TABLE IF NOT EXISTS core.login (
	id int4 NOT NULL GENERATED ALWAYS AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE),
	login varchar NOT NULL,
	auth_text varchar NOT NULL,
	CONSTRAINT login_pk PRIMARY KEY (id)
);
CREATE UNIQUE INDEX login_login_idx ON core.login USING btree (login);
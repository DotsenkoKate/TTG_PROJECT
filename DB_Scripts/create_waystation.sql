-- Table: public.waystation

-- DROP TABLE public.waystation;

CREATE TABLE public.waystation
(
    id bigint NOT NULL DEFAULT nextval('waystation_id_seq'::regclass),
    name character varying(150) COLLATE pg_catalog."default" NOT NULL,
    place character varying(150) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT waystation_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE public.waystation
    OWNER to postgres;
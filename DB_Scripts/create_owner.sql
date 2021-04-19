-- Table: public.owner

-- DROP TABLE public.owner;

CREATE TABLE public.owner
(
    id bigint NOT NULL DEFAULT nextval('owner_id_seq'::regclass),
    name character varying(54) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT owner_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE public.owner
    OWNER to postgres;
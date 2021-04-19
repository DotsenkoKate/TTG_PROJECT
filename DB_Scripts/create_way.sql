-- Table: public.way

-- DROP TABLE public.way;

CREATE TABLE public.way
(
    "number" bigint NOT NULL,
    price integer,
    owner_id bigint,
    CONSTRAINT way_pkey PRIMARY KEY ("number"),
    CONSTRAINT way_owner_id_fkey FOREIGN KEY (owner_id)
        REFERENCES public.owner (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE public.way
    OWNER to postgres;
-- Table: public.way_waystation

-- DROP TABLE public.way_waystation;

CREATE TABLE public.way_waystation
(
    id bigint NOT NULL DEFAULT nextval('way_waystation_id_seq'::regclass),
    way_number bigint,
    waystation_id bigint,
    CONSTRAINT way_waystation_pkey PRIMARY KEY (id),
    CONSTRAINT way_waystation_way_number_fkey FOREIGN KEY (way_number)
        REFERENCES public.way ("number") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT way_waystation_waystation_id_fkey FOREIGN KEY (waystation_id)
        REFERENCES public.waystation (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE public.way_waystation
    OWNER to postgres;
CREATE SCHEMA IF NOT EXISTS "public";

CREATE OR REPLACE FUNCTION public.custom_seq(in_prefix character varying, in_sequence_name character varying, in_digit_count integer)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
DECLARE
    seq_value INT;
    result VARCHAR;
BEGIN
    EXECUTE 'SELECT nextval(''' || in_sequence_name || '''::regclass)' INTO seq_value;
    result := in_prefix || LPAD(seq_value::TEXT, in_digit_count, '0');
    RETURN result;
END;
$function$
;

CREATE SEQUENCE "public".departement_seq START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE "public".employee_seq START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE "public".person_seq START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE "public".product_seq START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE "public".proforma_details_seq START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE "public".proforma_seq START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE "public".purchase_order_details_seq START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE "public".purchase_order_seq START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE "public".request_details_seq START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE "public".request_seq START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE "public".session_id_seq START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE "public".supplier_product_seq START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE "public".supplier_seq START WITH 1 INCREMENT BY 1;

CREATE  TABLE "public".person (
	person_id            varchar DEFAULT custom_seq('PER'::character varying, 'person_seq'::character varying, 5) NOT NULL  ,
	first_name           varchar(50)    ,
	last_name            varchar(50)    ,
	date_of_birth        date    ,
	gender               varchar(10)    ,
	phone_number         varchar(20)    ,
	address              varchar(255)    ,
	CONSTRAINT person_pkey PRIMARY KEY ( person_id )
 );

CREATE  TABLE "public".product (
	product_id           varchar DEFAULT custom_seq('PRO'::character varying, 'product_seq'::character varying, 5) NOT NULL  ,
	product_name         varchar(100)    ,
	CONSTRAINT product_pkey PRIMARY KEY ( product_id ),
	CONSTRAINT product_product_name_key UNIQUE ( product_name )
 );

CREATE  TABLE "public"."session" (
	"value"              varchar    ,
	id                   integer DEFAULT nextval('session_id_seq'::regclass) NOT NULL  ,
	CONSTRAINT pk_session PRIMARY KEY ( id )
 );

CREATE  TABLE "public".supplier (
	supplier_id          varchar DEFAULT custom_seq('SUP'::character varying, 'supplier_seq'::character varying, 5) NOT NULL  ,
	name                 varchar(100)    ,
	contact_email        varchar(100)    ,
	contact_phone        varchar(20)    ,
	address              varchar(255)    ,
	CONSTRAINT supplier_pkey PRIMARY KEY ( supplier_id ),
	CONSTRAINT supplier_name_key UNIQUE ( name )
 );

CREATE  TABLE "public".supplier_product (
	supplier_product_id  varchar DEFAULT custom_seq('SPR'::character varying, 'supplier_product_seq'::character varying, 5) NOT NULL  ,
	supplier_id          varchar    ,
	product_id           varchar    ,
	CONSTRAINT supplier_product_pkey PRIMARY KEY ( supplier_product_id ),
	CONSTRAINT supplier_product_product_id_fkey FOREIGN KEY ( product_id ) REFERENCES "public".product( product_id )   ,
	CONSTRAINT supplier_product_supplier_id_fkey FOREIGN KEY ( supplier_id ) REFERENCES "public".supplier( supplier_id )
 );

CREATE  TABLE "public".department (
	department_id        varchar DEFAULT custom_seq('DEP'::character varying, 'departement_seq'::character varying, 5) NOT NULL  ,
	department_name      varchar(100)    ,
	department_head_id   varchar    ,
	CONSTRAINT department_pkey PRIMARY KEY ( department_id ),
	CONSTRAINT department_department_name_key UNIQUE ( department_name ) ,
	CONSTRAINT department_department_head_id_fkey FOREIGN KEY ( department_head_id ) REFERENCES "public".person( person_id )
 );

CREATE  TABLE "public".employee (
	employee_id          varchar DEFAULT custom_seq('EMP'::character varying, 'employee_seq'::character varying, 5) NOT NULL  ,
	person_id            varchar    ,
	department_id        varchar    ,
	hire_date            date    ,
	job_title            varchar(100)    ,
	salary               numeric(10,2)    ,
	email                varchar(100)    ,
	"password"           varchar(20)    ,
	daf                  boolean DEFAULT false   ,
	CONSTRAINT employee_pkey PRIMARY KEY ( employee_id ),
	CONSTRAINT employee_person_id_key UNIQUE ( person_id ) ,
	CONSTRAINT employee_department_id_fkey FOREIGN KEY ( department_id ) REFERENCES "public".department( department_id )   ,
	CONSTRAINT employee_person_id_fkey FOREIGN KEY ( person_id ) REFERENCES "public".person( person_id )
 );

CREATE  TABLE "public".proforma (
	proforma_id          varchar DEFAULT custom_seq('PRO'::character varying, 'proforma_seq'::character varying, 5) NOT NULL  ,
	issue_date           date    ,
	due_date             date    ,
	supplier_id          varchar    ,
	CONSTRAINT proforma_pkey PRIMARY KEY ( proforma_id ),
	CONSTRAINT proforma_supplier_id_fkey FOREIGN KEY ( supplier_id ) REFERENCES "public".supplier( supplier_id )
 );

CREATE  TABLE "public".proforma_details (
	proforma_details_id  varchar DEFAULT custom_seq('PRD'::character varying, 'proforma_details_seq'::character varying, 5) NOT NULL  ,
	proforma_id          varchar    ,
	product_id           varchar    ,
	quantity             integer    ,
	price                numeric(10,2)    ,
	CONSTRAINT proforma_details_pkey PRIMARY KEY ( proforma_details_id ),
	CONSTRAINT proforma_details_product_id_fkey FOREIGN KEY ( product_id ) REFERENCES "public".product( product_id )   ,
	CONSTRAINT proforma_details_proforma_id_fkey FOREIGN KEY ( proforma_id ) REFERENCES "public".proforma( proforma_id )
 );

CREATE  TABLE "public".purchase_order (
	purchase_order_id    varchar DEFAULT custom_seq('PUR'::character varying, 'purchase_order_seq'::character varying, 5) NOT NULL  ,
	created_at           timestamp(0) DEFAULT CURRENT_TIMESTAMP   ,
	delivery_days        integer DEFAULT 30   ,
	supplier_id          varchar    ,
	validation           integer DEFAULT 10   ,
	CONSTRAINT purchase_order_pkey PRIMARY KEY ( purchase_order_id ),
	CONSTRAINT purchase_order_supplier_id_fkey FOREIGN KEY ( supplier_id ) REFERENCES "public".supplier( supplier_id )
 );

CREATE  TABLE "public".purchase_order_details (
	purchase_order_details_id varchar DEFAULT custom_seq('PUD'::character varying, 'purchase_order_details_seq'::character varying, 5) NOT NULL  ,
	purchase_order_id    varchar    ,
	product_id           varchar    ,
	quantity             double precision    ,
	price                numeric(10,2)    ,
	CONSTRAINT purchase_order_details_pkey PRIMARY KEY ( purchase_order_details_id ),
	CONSTRAINT purchase_order_details_product_id_fkey FOREIGN KEY ( product_id ) REFERENCES "public".product( product_id )   ,
	CONSTRAINT purchase_order_details_purchase_order_id_fkey FOREIGN KEY ( purchase_order_id ) REFERENCES "public".purchase_order( purchase_order_id )
 );

CREATE  TABLE "public".request (
	request_id           varchar DEFAULT custom_seq('REQ'::character varying, 'request_seq'::character varying, 5) NOT NULL  ,
	department_id        varchar    ,
	created_at           date    ,
	is_validated         boolean DEFAULT false   ,
	employee_id          varchar    ,
	CONSTRAINT request_pkey PRIMARY KEY ( request_id ),
	CONSTRAINT request_department_id_fkey FOREIGN KEY ( department_id ) REFERENCES "public".department( department_id )   ,
	CONSTRAINT fk_request_employee FOREIGN KEY ( employee_id ) REFERENCES "public".employee( employee_id )
 );

CREATE  TABLE "public".request_details (
	request_details_id   varchar DEFAULT custom_seq('RED'::character varying, 'request_details_seq'::character varying, 5) NOT NULL  ,
	request_id           varchar    ,
	product_id           varchar    ,
	quantity             integer    ,
	reason               varchar(200)    ,
	is_validated         boolean DEFAULT false   ,
	departement_name     varchar(100)    ,
	treated              boolean DEFAULT false NOT NULL  ,
	CONSTRAINT request_details_pkey PRIMARY KEY ( request_details_id ),
	CONSTRAINT request_details_product_id_fkey FOREIGN KEY ( product_id ) REFERENCES "public".product( product_id )   ,
	CONSTRAINT request_details_request_id_fkey FOREIGN KEY ( request_id ) REFERENCES "public".request( request_id )
 );



CREATE VIEW "public".v_product_necessary AS  SELECT e.product_id,
    e.quantity,
    p.product_name
   FROM (( SELECT rd.product_id,
            sum(rd.quantity) AS quantity
           FROM request_details rd
          WHERE ((rd.treated = false) AND (rd.is_validated = true))
          GROUP BY rd.product_id) e
     JOIN product p ON (((e.product_id)::text = (p.product_id)::text)));

CREATE VIEW "public".v_proforma_moins_disant AS  SELECT v.proforma_details_id,
    v.proforma_id,
    v.product_id,
    v.quantity,
    v.price,
    p.supplier_id
   FROM (( SELECT pd.proforma_details_id,
            pd.proforma_id,
            pd.product_id,
            pd.quantity,
            pd.price
           FROM (proforma_details pd
             JOIN ( SELECT pd_1.product_id,
                    min(pd_1.price) AS min_price
                   FROM proforma_details pd_1
                  GROUP BY pd_1.product_id) e ON (((pd.product_id)::text = (e.product_id)::text)))
          WHERE (e.min_price = pd.price)) v
     JOIN proforma p ON (((p.proforma_id)::text = (v.proforma_id)::text)));

SELECT d.department_id,
    p.product_id,
    d.department_name,
    p.product_name,
    to_char(date_trunc('month', r.created_at), 'Month YYYY') AS month,
    sum(rd.quantity) AS total_quantity
   FROM (((request r
     JOIN request_details rd ON (((rd.request_id) = (r.request_id))))
     JOIN department d ON (((d.department_id) = (r.department_id))))
     JOIN product p ON (((p.product_id) = (rd.product_id))))
  WHERE (r.is_validated = true)
  GROUP BY d.department_id, (to_char(date_trunc('month', r.created_at), 'Month YYYY')), p.product_id
  ORDER BY (to_char(date_trunc('month', r.created_at), 'Month YYYY')) DESC


INSERT INTO "public".person( person_id, first_name, last_name, date_of_birth, gender, phone_number, address ) VALUES ( 'PER00001', 'RATIATIANA', 'Jean Mirlin', '2023-10-20', 'M', '0348262182', 'Ambohijanaka');
INSERT INTO "public".person( person_id, first_name, last_name, date_of_birth, gender, phone_number, address ) VALUES ( 'PER00002', 'RAMIANDRISOA', 'Tiavina', '2023-01-20', 'M', '+261 32 500 48', 'Tanjombato');
INSERT INTO "public".person( person_id, first_name, last_name, date_of_birth, gender, phone_number, address ) VALUES ( 'PER00003', 'RAMAROSON', 'Benjamina', '2023-10-04', 'M', '+261 02 591 02', 'Iavoloha');
INSERT INTO "public".person( person_id, first_name, last_name, date_of_birth, gender, phone_number, address ) VALUES ( 'PER00004', 'RAMIANDRISOA', 'Lina', '2023-01-20', 'F', '+261 32 520 48', 'Antananarivo');
INSERT INTO "public".product( product_id, product_name ) VALUES ( 'PRO00001', 'ordinateur');
INSERT INTO "public".product( product_id, product_name ) VALUES ( 'PRO00002', 'projecteur');
INSERT INTO "public".product( product_id, product_name ) VALUES ( 'PRO00003', 'Souris');
INSERT INTO "public".product( product_id, product_name ) VALUES ( 'PRO00004', 'Telephone');
INSERT INTO "public".product( product_id, product_name ) VALUES ( 'PRO00005', 'Prise');
INSERT INTO "public".product( product_id, product_name ) VALUES ( 'PRO00006', 'Clavier');
INSERT INTO "public".product( product_id, product_name ) VALUES ( 'PRO00007', 'Gomme');
INSERT INTO "public".product( product_id, product_name ) VALUES ( 'PRO00008', 'Carnet');
INSERT INTO "public".product( product_id, product_name ) VALUES ( 'PRO00009', 'Classeur');
INSERT INTO "public".product( product_id, product_name ) VALUES ( 'PRO00010', 'Porte document');
INSERT INTO "public"."session"( "value", id ) VALUES ( 'PER00002', 1);
INSERT INTO "public".supplier( supplier_id, name, contact_email, contact_phone, address ) VALUES ( 'SUP00001', 'Asus', 'asus.as@gmail.com', '0345467687', 'Tana');
INSERT INTO "public".supplier( supplier_id, name, contact_email, contact_phone, address ) VALUES ( 'SUP00002', 'Hp', 'hp.hp@gmail.com', '0345467687', 'Tana');
INSERT INTO "public".supplier( supplier_id, name, contact_email, contact_phone, address ) VALUES ( 'SUP00003', 'Jumbo Score', 'jumbo@gmail.com', '+261 34 11 111 11', 'Tanjombato');
INSERT INTO "public".supplier( supplier_id, name, contact_email, contact_phone, address ) VALUES ( 'SUP00004', 'Shoprite', 'shoprite@gmail.com', '+261 33 25 255 25', 'Analakely');
INSERT INTO "public".supplier( supplier_id, name, contact_email, contact_phone, address ) VALUES ( 'SUP00005', 'Leader Price', 'leader@gmail.com', '+261 32 22 222 55', 'Ankadimbahoaka');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00001', 'SUP00001', 'PRO00001');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00002', 'SUP00002', 'PRO00002');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00021', 'SUP00003', 'PRO00003');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00022', 'SUP00004', 'PRO00004');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00023', 'SUP00005', 'PRO00005');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00024', 'SUP00003', 'PRO00006');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00025', 'SUP00004', 'PRO00007');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00026', 'SUP00001', 'PRO00008');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00027', 'SUP00002', 'PRO00009');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00028', 'SUP00002', 'PRO00010');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00029', 'SUP00001', 'PRO00002');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00030', 'SUP00001', 'PRO00003');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00031', 'SUP00001', 'PRO00004');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00032', 'SUP00005', 'PRO00001');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00033', 'SUP00005', 'PRO00002');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00034', 'SUP00005', 'PRO00010');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00035', 'SUP00004', 'PRO00001');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00036', 'SUP00004', 'PRO00009');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00037', 'SUP00002', 'PRO00004');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00038', 'SUP00002', 'PRO00004');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00039', 'SUP00003', 'PRO00002');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00040', 'SUP00003', 'PRO00005');
INSERT INTO "public".supplier_product( supplier_product_id, supplier_id, product_id ) VALUES ( 'SPR00041', 'SUP00003', 'PRO00010');
INSERT INTO "public".department( department_id, department_name, department_head_id ) VALUES ( 'DEP00001', 'Informatique', 'PER00001');
INSERT INTO "public".department( department_id, department_name, department_head_id ) VALUES ( 'DEP00005', 'RH', 'PER00001');
INSERT INTO "public".department( department_id, department_name, department_head_id ) VALUES ( 'DEP00006', 'Commercial', 'PER00001');
INSERT INTO "public".department( department_id, department_name, department_head_id ) VALUES ( 'DEP00007', 'Marketing', 'PER00001');
INSERT INTO "public".employee( employee_id, person_id, department_id, hire_date, job_title, salary, email, "password", daf ) VALUES ( 'EMP00001', 'PER00001', 'DEP00001', '2022-12-12', 'DEV', 2000000, 'jeanmirlin.r@gmail.com', 'mirlin', false);
INSERT INTO "public".employee( employee_id, person_id, department_id, hire_date, job_title, salary, email, "password", daf ) VALUES ( 'EMP00002', 'PER00002', 'DEP00005', '2023-11-20', 'Responsable', 3000000, 'ramiandrisoatiavina@gmail.com', 'tiavina', false);
INSERT INTO "public".employee( employee_id, person_id, department_id, hire_date, job_title, salary, email, "password", daf ) VALUES ( 'EMP00003', 'PER00003', 'DEP00006', '2023-10-15', 'Chef', 2500000, 'ramarosonbenjamina@gmail.com', 'ben', false);
INSERT INTO "public".employee( employee_id, person_id, department_id, hire_date, job_title, salary, email, "password", daf ) VALUES ( 'EMP00004', 'PER00004', 'DEP00007', '2023-10-10', 'Superviseur', 2000000, 'lina@gmail.com', 'lina', false);
INSERT INTO "public".proforma( proforma_id, issue_date, due_date, supplier_id ) VALUES ( 'PRO00016', null, null, 'SUP00001');
INSERT INTO "public".proforma( proforma_id, issue_date, due_date, supplier_id ) VALUES ( 'PRO00017', null, null, 'SUP00002');
INSERT INTO "public".proforma( proforma_id, issue_date, due_date, supplier_id ) VALUES ( 'PRO00018', null, null, 'SUP00003');
INSERT INTO "public".proforma( proforma_id, issue_date, due_date, supplier_id ) VALUES ( 'PRO00019', null, null, 'SUP00004');
INSERT INTO "public".proforma( proforma_id, issue_date, due_date, supplier_id ) VALUES ( 'PRO00020', null, null, 'SUP00005');
INSERT INTO "public".proforma_details( proforma_details_id, proforma_id, product_id, quantity, price ) VALUES ( 'PRD00024', 'PRO00016', 'PRO00008', 500, 200);
INSERT INTO "public".proforma_details( proforma_details_id, proforma_id, product_id, quantity, price ) VALUES ( 'PRD00025', 'PRO00016', 'PRO00001', 100, 1500000);
INSERT INTO "public".proforma_details( proforma_details_id, proforma_id, product_id, quantity, price ) VALUES ( 'PRD00026', 'PRO00016', 'PRO00003', 100, 5000);
INSERT INTO "public".proforma_details( proforma_details_id, proforma_id, product_id, quantity, price ) VALUES ( 'PRD00027', 'PRO00016', 'PRO00002', 10, 1500000);
INSERT INTO "public".proforma_details( proforma_details_id, proforma_id, product_id, quantity, price ) VALUES ( 'PRD00028', 'PRO00016', 'PRO00004', 100, 2000000);
INSERT INTO "public".proforma_details( proforma_details_id, proforma_id, product_id, quantity, price ) VALUES ( 'PRD00029', 'PRO00017', 'PRO00002', 200, 1000000);
INSERT INTO "public".proforma_details( proforma_details_id, proforma_id, product_id, quantity, price ) VALUES ( 'PRD00030', 'PRO00017', 'PRO00010', 500, 1000);
INSERT INTO "public".proforma_details( proforma_details_id, proforma_id, product_id, quantity, price ) VALUES ( 'PRD00031', 'PRO00017', 'PRO00009', 400, 4000);
INSERT INTO "public".proforma_details( proforma_details_id, proforma_id, product_id, quantity, price ) VALUES ( 'PRD00032', 'PRO00017', 'PRO00004', 80, 1200000);
INSERT INTO "public".proforma_details( proforma_details_id, proforma_id, product_id, quantity, price ) VALUES ( 'PRD00033', 'PRO00018', 'PRO00002', 500, 5000000);
INSERT INTO "public".proforma_details( proforma_details_id, proforma_id, product_id, quantity, price ) VALUES ( 'PRD00034', 'PRO00018', 'PRO00003', 200, 3000);
INSERT INTO "public".proforma_details( proforma_details_id, proforma_id, product_id, quantity, price ) VALUES ( 'PRD00035', 'PRO00018', 'PRO00006', 200, 6000);
INSERT INTO "public".proforma_details( proforma_details_id, proforma_id, product_id, quantity, price ) VALUES ( 'PRD00036', 'PRO00018', 'PRO00005', 200, 1500);
INSERT INTO "public".proforma_details( proforma_details_id, proforma_id, product_id, quantity, price ) VALUES ( 'PRD00037', 'PRO00018', 'PRO00010', 400, 6000);
INSERT INTO "public".proforma_details( proforma_details_id, proforma_id, product_id, quantity, price ) VALUES ( 'PRD00038', 'PRO00019', 'PRO00001', 30, 1600000);
INSERT INTO "public".proforma_details( proforma_details_id, proforma_id, product_id, quantity, price ) VALUES ( 'PRD00039', 'PRO00019', 'PRO00004', 120, 400000);
INSERT INTO "public".proforma_details( proforma_details_id, proforma_id, product_id, quantity, price ) VALUES ( 'PRD00040', 'PRO00019', 'PRO00009', 200, 500);
INSERT INTO "public".proforma_details( proforma_details_id, proforma_id, product_id, quantity, price ) VALUES ( 'PRD00041', 'PRO00019', 'PRO00007', 200, 200);
INSERT INTO "public".proforma_details( proforma_details_id, proforma_id, product_id, quantity, price ) VALUES ( 'PRD00042', 'PRO00020', 'PRO00002', 20, 1800000);
INSERT INTO "public".proforma_details( proforma_details_id, proforma_id, product_id, quantity, price ) VALUES ( 'PRD00043', 'PRO00020', 'PRO00001', 20, 900000);
INSERT INTO "public".proforma_details( proforma_details_id, proforma_id, product_id, quantity, price ) VALUES ( 'PRD00044', 'PRO00020', 'PRO00005', 100, 2000);
INSERT INTO "public".purchase_order( purchase_order_id, created_at, delivery_days, supplier_id, validation ) VALUES ( 'PUR00009', '2023-11-20 01:44:23 PM', 30, 'SUP00002', 10);
INSERT INTO "public".purchase_order( purchase_order_id, created_at, delivery_days, supplier_id, validation ) VALUES ( 'PUR00010', '2023-11-20 01:44:48 PM', 30, 'SUP00002', 20);
INSERT INTO "public".purchase_order_details( purchase_order_details_id, purchase_order_id, product_id, quantity, price ) VALUES ( 'PUD00016', 'PUR00009', 'PRO00002', 1500.0, 150);
INSERT INTO "public".purchase_order_details( purchase_order_details_id, purchase_order_id, product_id, quantity, price ) VALUES ( 'PUD00017', 'PUR00010', 'PRO00002', 1500.0, 150);
INSERT INTO "public".request( request_id, department_id, created_at, is_validated, employee_id ) VALUES ( 'REQ00035', 'DEP00001', '2023-11-20', true, 'EMP00001');
INSERT INTO "public".request( request_id, department_id, created_at, is_validated, employee_id ) VALUES ( 'REQ00036', 'DEP00005', '2023-11-20', true, 'EMP00001');
INSERT INTO "public".request( request_id, department_id, created_at, is_validated, employee_id ) VALUES ( 'REQ00037', 'DEP00001', '2023-11-20', false, 'EMP00001');
INSERT INTO "public".request_details( request_details_id, request_id, product_id, quantity, reason, is_validated, departement_name, treated ) VALUES ( 'RED00032', 'REQ00035', 'PRO00003', 2, 'Manque', true, null, false);
INSERT INTO "public".request_details( request_details_id, request_id, product_id, quantity, reason, is_validated, departement_name, treated ) VALUES ( 'RED00033', 'REQ00035', 'PRO00001', 2, 'manque', true, null, false);
INSERT INTO "public".request_details( request_details_id, request_id, product_id, quantity, reason, is_validated, departement_name, treated ) VALUES ( 'RED00034', 'REQ00036', 'PRO00002', 10, 'ordi', true, null, false);
INSERT INTO "public".request_details( request_details_id, request_id, product_id, quantity, reason, is_validated, departement_name, treated ) VALUES ( 'RED00035', 'REQ00036', 'PRO00007', 20, 'gomme', true, null, false);
INSERT INTO "public".request_details( request_details_id, request_id, product_id, quantity, reason, is_validated, departement_name, treated ) VALUES ( 'RED00036', 'REQ00037', 'PRO00007', 10, 'higommina', false, null, false);

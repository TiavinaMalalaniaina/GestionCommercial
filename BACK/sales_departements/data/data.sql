--CREATE DATABASE sales_departement

CREATE OR REPLACE FUNCTION custom_seq(
    in_prefix VARCHAR,
    in_sequence_name VARCHAR,
    in_digit_count INT
) RETURNS VARCHAR AS $$
DECLARE
    seq_value INT;
    result VARCHAR;
BEGIN
    EXECUTE 'SELECT nextval(''' || in_sequence_name || '''::regclass)' INTO seq_value;
    result := in_prefix || LPAD(seq_value::TEXT, in_digit_count, '0');
    RETURN result;
END;
$$ LANGUAGE plpgsql;

CREATE SEQUENCE person_seq START 1;
CREATE SEQUENCE departement_seq START 1;
CREATE SEQUENCE employee_seq START 1;
CREATE SEQUENCE supplier_seq START 1;
CREATE SEQUENCE product_seq START 1;
CREATE SEQUENCE supplier_product_seq START 1;
CREATE SEQUENCE proforma_seq START 1;
CREATE SEQUENCE proforma_details_seq START 1;
CREATE SEQUENCE purchase_order_seq START 1;
CREATE SEQUENCE purchase_order_details_seq START 1;
CREATE SEQUENCE request_seq START 1;
CREATE SEQUENCE request_details_seq START 1;



CREATE TABLE person (
    person_id VARCHAR PRIMARY KEY DEFAULT custom_seq('PER', 'person_seq', 5),
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    date_of_birth DATE,
    gender VARCHAR(10),
    email VARCHAR(100) UNIQUE,
    phone_number VARCHAR(20),
    address VARCHAR(255)
);

CREATE TABLE department (
    department_id VARCHAR PRIMARY KEY DEFAULT custom_seq('DEP', 'departement_seq', 5),
    department_name VARCHAR(100) UNIQUE,
    department_head_id VARCHAR,
    FOREIGN KEY (department_head_id) REFERENCES person(person_id)
);

CREATE TABLE employee (
    employee_id VARCHAR PRIMARY KEY DEFAULT custom_seq('EMP', 'employee_seq', 5),
    person_id VARCHAR UNIQUE,
    department_id VARCHAR,
    hire_date DATE,
    job_title VARCHAR(100),
    salary DECIMAL(10, 2),
    FOREIGN KEY (person_id) REFERENCES person(person_id),
    FOREIGN KEY (department_id) REFERENCES department(department_id)
);

CREATE TABLE supplier (
    supplier_id VARCHAR PRIMARY KEY DEFAULT custom_seq('SUP', 'supplier_seq', 5),
    name VARCHAR(100) UNIQUE,
    contact_email VARCHAR(100),
    contact_phone VARCHAR(20),
    address VARCHAR(255)
);

CREATE TABLE product (
    product_id VARCHAR PRIMARY KEY DEFAULT custom_seq('PRO', 'product_seq', 5),
    product_name VARCHAR(100) UNIQUE
);

CREATE TABLE supplier_product (
    supplier_product_id VARCHAR PRIMARY KEY DEFAULT custom_seq('SPR', 'supplier_product_seq', 5),
    supplier_id VARCHAR,
    product_id VARCHAR,
    FOREIGN KEY (supplier_id) REFERENCES supplier(supplier_id),
    FOREIGN KEY (product_id) REFERENCES product(product_id)
);

CREATE TABLE proforma (
    proforma_id VARCHAR PRIMARY KEY DEFAULT custom_seq('PRO', 'proforma_seq', 5),
    issue_date DATE,
    due_date DATE,
    supplier_id VARCHAR,
    FOREIGN KEY (supplier_id) REFERENCES supplier(supplier_id)
);

CREATE TABLE proforma_details (
    proforma_details_id VARCHAR PRIMARY KEY DEFAULT custom_seq('PRD', 'proforma_details_seq', 5),
    proforma_id VARCHAR,
    product_id VARCHAR,
    quantity INT,
    price DECIMAL(10, 2),
    FOREIGN KEY (proforma_id) REFERENCES proforma(proforma_id),
    FOREIGN KEY (product_id) REFERENCES product(product_id)
);

CREATE TABLE purchase_order (
    purchase_order_id VARCHAR PRIMARY KEY DEFAULT custom_seq('PUR', 'purchase_order_seq', 5),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    delivery_days INT,
    supplier_id VARCHAR,
    validation INT DEFAULT 10,
    FOREIGN KEY (supplier_id) REFERENCES supplier(supplier_id)
);

CREATE TABLE purchase_order_details (
    purchase_order_details_id VARCHAR PRIMARY KEY DEFAULT custom_seq('PUD', 'purchase_order_details_seq', 5),
    purchase_order_id VARCHAR,
    product_id VARCHAR,
    quantity DOUBLE PRECISION,
    price DECIMAL(10, 2),
    FOREIGN KEY (purchase_order_id) REFERENCES purchase_order(purchase_order_id),
    FOREIGN KEY (product_id) REFERENCES product(product_id)
);

CREATE TABLE request (
    request_id VARCHAR PRIMARY KEY DEFAULT custom_seq('REQ', 'request_seq', 5),
    department_id VARCHAR,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    is_validated BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (department_id) REFERENCES department(department_id)
);

CREATE TABLE request_details (
    request_details_id VARCHAR PRIMARY KEY DEFAULT custom_seq('RED', 'request_details_seq', 5),
    request_id VARCHAR,
    product_id VARCHAR,
    quantity INT,
    FOREIGN KEY (request_id) REFERENCES request(request_id),
    FOREIGN KEY (product_id) REFERENCES product(product_id)
);

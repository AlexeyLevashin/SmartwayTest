INSERT INTO departments (name, phone)
VALUES (@name, @phone)
    RETURNING 
    id AS "Id", 
    name AS "Name", 
    phone AS "Phone";
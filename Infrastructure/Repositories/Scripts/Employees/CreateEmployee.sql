INSERT INTO employees (name, surname, phone, company_id, department_id)
VALUES (@name, @surname, @phone, @companyId, @departmentId) 
    RETURNING
    id as "Id";
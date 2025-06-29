INSERT INTO passports (type, number, employee_id)
VALUES (@type, @number, @employeeId)
    RETURNING 
    id as "Id"
SELECT id AS "Id",
       type AS "Type",
       number AS "Number",
       employee_id AS "EmployeeId"
FROM passports
WHERE employee_id = @employeeId;
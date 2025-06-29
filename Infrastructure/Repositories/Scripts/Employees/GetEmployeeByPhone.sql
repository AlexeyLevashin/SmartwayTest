SELECT id as "Id",
       name as "Name",
       surname as "Surname",
       phone as "Phone",
       company_id as "CompanyId"
FROM employees WHERE phone=@phone
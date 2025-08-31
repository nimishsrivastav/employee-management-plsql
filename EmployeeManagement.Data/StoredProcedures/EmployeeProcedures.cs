using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.StoredProcedures
{
    public class EmployeeProcedures
    {
        public static string CreateGetEmployeesByDepartmentProcedure()
        {
            return @"
            CREATE OR REPLACE PROCEDURE GET_EMPLOYEES_BY_DEPT(
                p_department_id IN NUMBER,
                p_cursor OUT SYS_REFCURSOR
            )
            AS
            BEGIN
                OPEN p_cursor FOR
                    SELECT 
                        e.ID,
                        e.FIRST_NAME,
                        e.LAST_NAME,
                        e.EMAIL,
                        e.SALARY,
                        e.HIRE_DATE,
                        e.CREATED_DATE,
                        e.MODIFIED_DATE,
                        e.DEPARTMENT_ID,
                        d.NAME as DEPARTMENT_NAME
                    FROM EMPLOYEES e
                    INNER JOIN DEPARTMENTS d ON e.DEPARTMENT_ID = d.ID
                    WHERE e.DEPARTMENT_ID = p_department_id
                    ORDER BY e.LAST_NAME, e.FIRST_NAME;
            END;
        ";
        }

        public static string CreateEmployeeUpdateTrigger()
        {
            return @"
            CREATE OR REPLACE TRIGGER EMPLOYEE_UPDATE_TRIGGER
                BEFORE UPDATE ON EMPLOYEES
                FOR EACH ROW
            BEGIN
                :NEW.MODIFIED_DATE := SYSDATE;
            END;
        ";
        }

        public static string CreateSequences()
        {
            return @"
            -- Create sequences for primary keys
            CREATE SEQUENCE EMPLOYEES_SEQ
                START WITH 1
                INCREMENT BY 1
                NOCACHE;

            CREATE SEQUENCE DEPARTMENTS_SEQ
                START WITH 4  -- Starting from 4 since we have 3 seed departments
                INCREMENT BY 1
                NOCACHE;
        ";
        }
    }
}

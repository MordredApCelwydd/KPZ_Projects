import React, { useEffect, useState } from "react";
import "../custom.css";

const URL = "api/employees";

function Employees() {
  const [allEmployees, setEmployees] = useState([]);

  const getEmployees = async () => {
    const options = {
      method: "GET",
    };
    const result = await fetch(URL, options);

    if (result.ok) {
      const employees = await result.json();
      setEmployees(employees);
      console.log("All employees:");
      console.log(allEmployees);
      return employees;
    }
    return [];
  };

  const addEmployee = async () => {
    const employeeName = document.querySelector("#employee-name").value;
    const employeeSurname = document.querySelector("#employee-surname").value;
    const employeePosition = document.querySelector("#employee-position").value;
    const employeeSalary = document.querySelector("#employee-salary").value;

    const newEmployee = {
      name: employeeName,
      surname: employeeSurname,
      position: employeePosition,
      salary: +employeeSalary,
    };

    const headers = new Headers();
    headers.set("Content-Type", "application/json");
    const options = {
      method: "POST",
      headers: headers,
      body: JSON.stringify(newEmployee),
    };

    const result = await fetch(URL, options);
    if (result.ok) {
      const employee = await result.json();
      allEmployees.push(employee);
      setEmployees(allEmployees.slice());
    }
  };

  const deleteEmployee = async (id) => {
    const options = {
      method: "DELETE",
      header: new Headers(),
    };
    await fetch(URL + `/${id}`, options);

    setEmployees(allEmployees.filter((x) => x.id !== id));
  };

  const updateEmployee = async (oldEmployee) => {
    const employeeName = document.querySelector("#employee-name").value;
    const employeeSurname = document.querySelector("#employee-surname").value;
    const employeePosition = document.querySelector("#employee-position").value;
    const employeeSalary = document.querySelector("#employee-salary").value;

    oldEmployee.name = employeeName;
    oldEmployee.surname = employeeSurname;
    oldEmployee.position = employeePosition;
    oldEmployee.salary = employeeSalary;

    const headers = new Headers();
    headers.set("Content-Type", "application/json");
    const options = {
      method: "PATCH",
      headers: headers,
      body: JSON.stringify(oldEmployee),
    };

    const result = await fetch(URL, options);
    if (result.ok) {
      const employee = await result.json();
      const updatedEmployeeIndex = allEmployees.findIndex(
        (x) => x.id == employee.id
      );
      allEmployees[updatedEmployeeIndex] = employee;
      setEmployees(allEmployees.slice());
    }
  };

  useEffect(() => {
    getEmployees();
  }, []);

  return (
    <>
      <h3 className="text-lg font-semibold mb-8">Employees</h3>
      <div className="flex gap-16">
        <div className="flex flex-col gap-3 w-[400px] mb-8 order-last">
          <div className="flex flex-col gap-2">
            <label for="employee-name">Employee name</label>
            <input
              className="border-2 border-solid border-black"
              id="employee-name"
              type="text"
            />
          </div>
          <div className="flex flex-col gap-2">
            <label for="employee-surname">Employee surname</label>
            <textarea
              className="border-2 border-solid border-black"
              id="employee-surname"
            />
          </div>
          <div className="flex flex-col gap-2">
            <label for="employee-position gap-2">Employee position</label>
            <input
              className="border-2 border-solid border-black"
              id="employee-position"
              type="text"
            />
          </div>
          <div className="flex flex-col gap-2">
            <label for="employee-salary">Employee salary</label>
            <input
              className="border-2 border-solid border-black"
              id="employee-salary"
              type="number"
            />
          </div>
          <div className="text-center mt-2">
            <button
              className="bg-sky-500 px-16 py-3 text-white rounded-md hover:bg-sky-600"
              onClick={addEmployee}
            >
              Add Employee
            </button>
          </div>
        </div>
        <div className="h-[500px] overflow-y-auto">
          <table className="w-[800px]">
            <thead>
              <tr>
                <th className="p-2 text-center border-1 border-black">Name</th>
                <th className="p-2 text-center border-1 border-black">
                  Surname
                </th>
                <th className="p-2 text-center border-1 border-black">
                  Position
                </th>
                <th className="p-2 text-center border-1 border-black">
                  Salary
                </th>
                <th className="p-2 text-center border-1 border-black"></th>
              </tr>
            </thead>
            <tbody>
              {allEmployees.map((x) => (
                <EmployeeRow
                  key={x.id}
                  employee={{
                    name: x.name,
                    surname: x.surname,
                    position: x.position,
                    salary: x.salary,
                  }}
                  deleteAction={() => deleteEmployee(x.id)}
                  updateAction={() => updateEmployee(x)}
                />
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </>
  );
}

export default Employees;

const EmployeeRow = ({ employee, deleteAction, updateAction }) => {
  return (
    <tr>
      <td className="p-2 text-center border-1 border-black">
        {" "}
        {employee.name}
      </td>
      <td className="p-2 text-center border-1 border-black">
        {employee.surname}
      </td>
      <td className="p-2 text-center border-1 border-black">
        {employee.position}
      </td>
      <td className="p-2 text-center border-1 border-black">
        {employee.salary}
      </td>
      <td className="p-2 text-center border-1 border-black">
        <div className="flex gap-2 justify-center">
          <button
            onClick={deleteAction}
            className="border-black border-2 border-solid rounded-sm hover:bg-slate-400"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              stroke-width="1.5"
              stroke="currentColor"
              class="w-6 h-6"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                d="M14.74 9l-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 01-2.244 2.077H8.084a2.25 2.25 0 01-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 00-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 013.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 00-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 00-7.5 0"
              />
            </svg>
          </button>
          <button
            onClick={updateAction}
            className="border-black border-2 border-solid rounded-sm hover:bg-slate-400"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              strokeWidth={1.5}
              stroke="currentColor"
              className="w-6 h-6"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M16.023 9.348h4.992v-.001M2.985 19.644v-4.992m0 0h4.992m-4.993 0l3.181 3.183a8.25 8.25 0 0013.803-3.7M4.031 9.865a8.25 8.25 0 0113.803-3.7l3.181 3.182m0-4.991v4.99"
              />
            </svg>
          </button>
        </div>
      </td>
    </tr>
  );
};

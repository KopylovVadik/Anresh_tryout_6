
let param = window.location.href.includes("employees") ? "employees" : "department";


async function GetData() {
    let data;

   
    const response = await fetch("/api/"+param,
        {
            method: "GET",
            headers: { "Accept": "application/json" }
        });

    
    if (response.ok === true) {
        data = await response.json();
        console.log(data);

    }

    if (param == "department") {

       

        getTableDepartment();
    }
    function getTableDepartment() {

        const tbody = document.querySelector('#body_table');
        let newElement = ' ';
        for (const department of data) {
            newElement += `<tr> <td>  ${department.id}  </td>  <td> <input type="text" value="${department.name}" onchange="editDepartment(${department.id},'name',this.value)">  </input> </td> <td> <button onclick="deleteDepartment(${department.id})">Удалить</button></td>  </tr>`;

        }
        tbody.innerHTML = newElement;
    }

    if (param == "employees") {

        getSelectDepartments();

        getTableEmployees();
    }

    function getTableEmployees() {

        const tbody = document.querySelector('#body_table_emploes');
        let newElement = ' ';
        for (const employees of data) {

            newElement += `<tr> <td>  <input type="text" value="${employees.fio}" onchange="editEmployee(${employees.id},'fio',this.value)">  </input> </td>  <td>  ${employees.dp[0].name} </td> <td> ${employees.salary}</td><td><button class="delete_empoyee" onclick="deleteEmployee(${employees.id})">Удалить</button></td>  </tr>`;

        }
        tbody.innerHTML = newElement;
    }

    async function getSelectDepartments() {
        let departmentsData;

        const response = await fetch("/api/department",
            {
                method: "GET",
                headers: { "Accept": "application/json" }
            });

        if (response.ok === true) {
            departmentsData = await response.json();
           

        }

         const select = document.querySelector('#select_department');
        newElement = '';

        for (const department of departmentsData) {
            
                newElement += `<option value="${department.id}"> ${department.name} </option>`;
            
        }
        select.innerHTML = newElement;
         
    }

     


}


GetData();



async function editDepartment(id,key, value) {
    const response = await fetch("/api/department",
        {
            method: "PUT",
            headers: { "Accept": "application/json" },

            body: new URLSearchParams({
                'id': id,
                'key': key,
                'value': value
})
        });

}

async function deleteDepartment(id) {

    const response = await fetch("/api/department",
        {
            method: "DELETE",
            headers: { "Accept": "application/json" },

            body: new URLSearchParams({

                'id': id
            })
        });
}

async function editEmployee(id,key,value) {
    const response = await fetch("/api/employees",
        {
            method: "PUT",
            headers: { "Accept": "application/json" },

            body: new URLSearchParams({
                'id': id,
                'key': key,
                'value': value
            })
        });
}

async function deleteEmployee(id) {
    const response = await fetch("/api/employees",
        {
            method: "DELETE",
            headers: { "Accept": "application/json" },

            body: new URLSearchParams({

                'id': id
            })
        });
}

async function addDepartment() {
     const response = await fetch("/api/department",
         {
             method: "POST",
             headers: { "Accept": "application/json" },
            
             body: new URLSearchParams({
                 
                 'name': document.querySelector("#department_input").value
             })
         });
 }

async function addEmployee() {
    const response = await fetch("/api/employees",
        {
            method: "POST",
            headers: { "Accept": "application/json" },

            body: new URLSearchParams({

                'id_department': document.querySelector("#select_department").value,
                'fio': document.querySelector("#employee_input").value,
                'salary': document.querySelector('#salary_input').value


            })
        });
   
}
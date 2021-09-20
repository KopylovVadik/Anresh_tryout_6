
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

        getTableEmployees();
    }

    function getTableEmployees() {

        const tbody = document.querySelector('#body_table_emploes');
        let newElement = ' ';
        for (const employees of data) {
            newElement += `<tr> <td>  ${employees.fio}  </td>  <td>  ${employees.dp[0].name} </td> <td> <button onclick="deleteEmployee(${employees.id})">Удалить</button></td>  </tr>`;

        }
        tbody.innerHTML = newElement;
    }


}


GetData();





function editDepartment(id,key, value) {
    console.log(key,value);

}

function deleteDepartment(id) {
    console.log(id);

}

function editEmployees(id,key,value) {
    console.log(key, value);

}

function deleteEmployee(id) {
    console.log(id);
}
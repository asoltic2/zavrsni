async function fetchEmployees() {
    const response = await fetch('/api/employee');
    const employees = await response.json();
    const tbody = document.querySelector('#employeeTable tbody');
    tbody.innerHTML = '';
    employees.forEach(employee => {
        const tr = document.createElement('tr');
        
        // Create cells for employee properties
        const tdId = document.createElement('td');
        tdId.textContent = employee.employeeId;
        tr.appendChild(tdId);
        
        const tdFirstName = document.createElement('td');
        tdFirstName.textContent = employee.firstName;
        tr.appendChild(tdFirstName);
        
        const tdLastName = document.createElement('td');
        tdLastName.textContent = employee.lastName;
        tr.appendChild(tdLastName);
        
        const tdEmail = document.createElement('td');
        tdEmail.textContent = employee.email;
        tr.appendChild(tdEmail);
        
        const tdDepartment = document.createElement('td');
        tdDepartment.textContent = employee.department;
        tr.appendChild(tdDepartment);
        
        // Create actions cell with Edit and Delete buttons
        const tdActions = document.createElement('td');
        
        const editButton = document.createElement('button');
        editButton.textContent = 'Edit';
        editButton.onclick = () => populateUpdateForm(employee);
        tdActions.appendChild(editButton);
        
        const deleteButton = document.createElement('button');
        deleteButton.textContent = 'Delete';
        deleteButton.onclick = () => deleteEmployee(employee.employeeId);
        tdActions.appendChild(deleteButton);
        
        tr.appendChild(tdActions);
        tbody.appendChild(tr);
    });
}

async function addEmployee() {
    const firstName = document.getElementById('firstName').value;
    const lastName = document.getElementById('lastName').value;
    const email = document.getElementById('email').value;
    const department = document.getElementById('department').value;

    await fetch('/api/employee', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ firstName, lastName, email, department })
    });
    fetchEmployees();
}

async function updateEmployee() {
    const id = parseInt(document.getElementById('updateId').value);
    const firstName = document.getElementById('updateFirstName').value;
    const lastName = document.getElementById('updateLastName').value;
    const email = document.getElementById('updateEmail').value;
    const department = document.getElementById('updateDepartment').value;

    await fetch(`/api/employee/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ employeeId: id, firstName, lastName, email, department })
    });
    fetchEmployees();
    hideUpdateForm();
}

async function deleteEmployee(id) {
    await fetch(`/api/employee/${id}`, {
        method: 'DELETE'
    });
    fetchEmployees();
}

// Populate the update form with the selected employee's data and show the form
function populateUpdateForm(employee) {
    document.getElementById('updateId').value = employee.employeeId;
    document.getElementById('updateFirstName').value = employee.firstName;
    document.getElementById('updateLastName').value = employee.lastName;
    document.getElementById('updateEmail').value = employee.email;
    document.getElementById('updateDepartment').value = employee.department;
    document.getElementById('updateForm').style.display = 'block';
}

// Hide the update form and clear its fields
function hideUpdateForm() {
    document.getElementById('updateForm').style.display = 'none';
    clearUpdateForm();
}

function clearUpdateForm() {
    document.getElementById('updateId').value = '';
    document.getElementById('updateFirstName').value = '';
    document.getElementById('updateLastName').value = '';
    document.getElementById('updateEmail').value = '';
    document.getElementById('updateDepartment').value = '';
}

// Initial load when the DOM is ready
document.addEventListener('DOMContentLoaded', fetchEmployees);

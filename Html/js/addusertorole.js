const roleid = localStorage.getItem('roleid');

async function CreateUserTable(loadroleid) {
    let urluserlist = "https://localhost:5101/api/Role/usernotinrolelist?id=" + loadroleid;

    let responseuserlist = await fetch(urluserlist, {
        method: "POST",
        headers: { "Content-Type": "application/json" }
    });

    if (responseuserlist.ok === true) {
        let users = await responseuserlist.json();
        let userrows = document.getElementById("selecttablebody");
        users.forEach(user => userrows.append(userrow(user)));

    }
    else {
        let error = await responseuserlist.json();
        console.log(error.message);
    }
}

function userrow(user) {
    const tr = document.createElement("tr");
    tr.setAttribute("data-rowid", user.id);
    const nameTd = document.createElement("td");

    nameTd.append(user.lastName + ' ' + user.name + ' ' + user.middleName);
    tr.append(nameTd);

    const idTd = document.createElement("td");
    idTd.append(user.id);
    idTd.hidden = true;
    tr.append(idTd);

    let emailTd = document.createElement("td");
    emailTd.append(user.email);
    tr.append(emailTd);

    const checkboxtd = document.createElement("td");
    const editLink = document.createElement("input");
    editLink.type = "checkbox";
    editLink.setAttribute("userchecked", user.id);
    checkboxtd.append(editLink);
    tr.append(checkboxtd);

    return tr;
}

async function AddUserRole(addroleid, adduserid) {
      let url = "https://localhost:5101/api/UserRole/adduserrole";

      let response = await fetch(url, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    userId: adduserid,
                    roleId: addroleid
                })
            });

       if (response.ok === true) {                
       }
       else {
                let error = await response.json();
                console.log(error.message);
        }
}

async function SaveSelectedUsers(addroleid) {
    const table = document.getElementById("selecttable");
    for (var i = 1, row; row = table.rows[i]; i++) {
        let adduserid = row.cells[1].innerHTML;
        let checkboxlength = row.querySelectorAll('input[type=checkbox]:checked').length;
        // Здесь вызываем, Web-api 
        if (checkboxlength == 1) {
            await AddUserRole(addroleid, adduserid);
        }
    }
    console.log("Переход на role");
    location.href = 'role.html';
}

window.onload = function () {
    CreateUserTable(roleid);
}

document.getElementById("saveusersbutton").addEventListener("click", async () => {
    await SaveSelectedUsers(roleid);
});
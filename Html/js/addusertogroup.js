const groupid = localStorage.getItem('groupid');

async function CreateUserTable(loadgroupid) {
    let urluserlist = "https://localhost:5101/api/Group/usernotingrouplist?id=" + loadgroupid;

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

async function AddUserGroup(addgroupid, adduserid) {
    let url = "https://localhost:5101/api/UserGroup/addusertogroup";

    let response = await fetch(url, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            userId: adduserid,
            groupId: addgroupid
        })
    });

    if (response.ok === true) {
    }
    else {
        let error = await response.json();
        console.log(error.message);    
    }
}

async function SaveSelectedUsers(addgroupid) {
    const table = document.getElementById("selecttable");
    for (var i = 1, row; row = table.rows[i]; i++) {
        let adduserid = row.cells[1].innerHTML;
        let checkboxlength = row.querySelectorAll('input[type=checkbox]:checked').length;
        // Çהוסü גûחûגאול, Web-api 
        if (checkboxlength == 1) {
            await AddUserGroup(addgroupid, adduserid);
        }
    }
    window.location.href = 'group.html';
}

window.onload = function () {
    CreateUserTable(groupid);
}

document.getElementById("saveusersbutton").addEventListener("click", async () => {
    await SaveSelectedUsers(groupid);

});
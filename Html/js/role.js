const roleid = localStorage.getItem('roleid');

async function loadroleinfo(loadroleid) {
    let url = "https://localhost:5101/api/Role/" + loadroleid;

    let response = await fetch(url, {
        method: "GET",
        headers: { "Content-Type": "application/json" }
    });

    if (response.ok === true) {
        let role = await response.json();
        document.getElementById("rolename").value = role.name;
    }
    else {
        let error = await response.json();
        console.log(error.message);
    }

    let urluserlist = "https://localhost:5101/api/Role/userlist?id=" + loadroleid;

    let responseuserlist = await fetch(urluserlist, {
        method: "POST",
        headers: { "Content-Type": "application/json" }
    });

    if (responseuserlist.ok === true) {
        let users = await responseuserlist.json();
        let userrows = document.getElementById("usertbody");
        users.forEach(user => userrows.append(userrow(user)));
    }
    else {
        let error = await responseuserlist.json();
        console.log(error.message);
    }

}

function userrow(user) {

    let tr = document.createElement("tr");
    tr.setAttribute("data-rowid", user.id);

    let lastnameTd = document.createElement("td");
    lastnameTd.append(user.lastName);
    tr.append(lastnameTd);

    let nameTd = document.createElement("td");
    nameTd.append(user.name);
    tr.append(nameTd);

    let middlenameTd = document.createElement("td");
    middlenameTd.append(user.middleName);
    tr.append(middlenameTd);

    let departmentTd = document.createElement("td");
    departmentTd.append(user.department);
    tr.append(departmentTd);

    let emailTd = document.createElement("td");
    emailTd.append(user.email);
    tr.append(emailTd);

    /*
    let linksTd = document.createElement("td");
 
    const removeLink = document.createElement("button");
    removeLink.append("�������");
    removeLink.addEventListener("click", async () => await deleteUser(user.id));
 
    linksTd.append(removeLink);
    tr.appendChild(linksTd);
    */

    let linksTd = document.createElement("td");

    const editLink = document.createElement("button");
    editLink.append("���������");
    editLink.className = "button";
    editLink.addEventListener("click", async () => await edituser(user.id));

    linksTd.append(editLink);
    tr.appendChild(linksTd);

    return tr;
}

function edituser(userid) {
    localStorage.setItem('userid', userid);
    window.location.href = 'user.html';
}

window.onload = function () {
    if (roleid != null) {
        document.getElementById("roleid").value = roleid;

        loadroleinfo(roleid);
    }


}

async function saveroleinfo(saveroleid, rolename) {
    let url = "https://localhost:5101/api/Role/" + saveroleid;

    let response = await fetch(url, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            name: rolename
        })
    });

    if (response.ok === true) {
        alert("������ ���������!");
    }
    else {
        const error = await response.json();
        console.log(error.message);
    }
}

document.getElementById("savebutton").addEventListener("click", async () => {
    let name = document.getElementById("rolename").value;
    await saveroleinfo(roleid, name);
});
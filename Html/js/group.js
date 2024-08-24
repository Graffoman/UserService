const groupid = localStorage.getItem('groupid');

async function loadgroupinfo(loadgroupid) {
    let url = "https://localhost:5101/api/Group/" + loadgroupid;

    let response = await fetch(url, {
        method: "GET",
        headers: { "Content-Type": "application/json" }
    });

    if (response.ok === true) {
        let group = await response.json();
        document.getElementById("groupname").value = group.name;
    }
    else {
        let error = await response.json();
        console.log(error.message);
    }

    let urluserlist = "https://localhost:5101/api/Group/userlist?id=" + loadgroupid;

    let responseuserlist = await fetch(urluserlist, {
        method: "GET",
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

async function deleteUserGroup(deleteuserid, deletegroupid) {
    let cr = confirm('Вы уверены, что хотите удалить пользователя из группы?');
    if (cr) {
        let url = "https://localhost:5101/api/UserGroup/deleteuserfromgroup";

        let response = await fetch(url, {
            method: "DELETE",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                userId: deleteuserid,
                groupId: deletegroupid
            })
        });

        if (response.ok === true) {
            location.reload();
        }
        else {
            let error = await response.json();
            console.log(error.message);
        }
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
    
    let linksTd = document.createElement("td");

    const editLink = document.createElement("button");
    editLink.append("Подробнее");
    editLink.className = "button";
    editLink.addEventListener("click", async () => await edituser(user.id));

    linksTd.append(editLink);
    tr.appendChild(linksTd);

    let removelinksTd = document.createElement("td");

    const removeLink = document.createElement("button");
    removeLink.className = "deletebutton";
    removeLink.append("Удалить");
    removeLink.addEventListener("click", async () => await deleteUserGroup(user.id, groupid));

    removelinksTd.append(removeLink);
    tr.appendChild(removelinksTd); 

    return tr;
}

function edituser(userid) {
    localStorage.setItem('userid', userid);
    window.location.href = 'user.html';
}

window.onload = function () {
    if (groupid != null) {
        document.getElementById("groupid").value = groupid;

        loadgroupinfo(groupid);
    }


}

async function savegroupinfo(savegroupid, groupname) {
    let url = "https://localhost:5101/api/Group/" + savegroupid;

    let response = await fetch(url, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            name: groupname
        })
    });

    if (response.ok === true) {
        console.log("Данные сохранены");
    }
    else {
        const error = await response.json();
        console.log(error.message);
    }
}

document.getElementById("savebutton").addEventListener("click", async () => {
    let groupname = document.getElementById("groupname").value;
    await savegroupinfo(groupid, groupname);
});

document.getElementById("adduserbutton").addEventListener("click", async () => {
    window.location.href = 'addusertogroup.html';
});
async function loadlistrole() {
    let url = "https://localhost:5101/api/Role/list";

    let response = await fetch(url, {
        method: "GET",
        headers: { "Content-Type": "application/json" }
    });

    if (response.ok === true) {
        let roles = await response.json();
        let rolerows = document.getElementById("roletbody");
        roles.forEach(role => rolerows.append(rolerow(role)));
    }
    else {
        let error = await response.json();
        console.log(error.message);
    }

}

async function deleterole(deleteroleid) {
    let cr = confirm('�� �������, ��� ������ ������� ����?');
    if (cr) {
        let url = "https://localhost:5101/api/Role/" + deleteroleid;

        let response = await fetch(url, {
            method: "DELETE",
            headers: { "Content-Type": "application/json" }
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

function rolerow(role) {

    let tr = document.createElement("tr");
    tr.setAttribute("data-rowid", role.id);

    let nameTd = document.createElement("td");
    nameTd.append(role.name);
    tr.append(nameTd);

    let linksTd = document.createElement("td");

    const editLink = document.createElement("button");
    editLink.append("���������");
    editLink.className = "button";
    editLink.addEventListener("click", async () => await editrole(role.id));

    linksTd.append(editLink);
    tr.appendChild(linksTd);

    let removelinksTd = document.createElement("td");

    const removeLink = document.createElement("button");
    removeLink.className = "deletebutton";
    removeLink.append("�������");
    removeLink.addEventListener("click", async () => await deleterole(role.id));

    removelinksTd.append(removeLink);
    tr.appendChild(removelinksTd); 

    return tr;
}

function editrole(roleid) {
    localStorage.setItem('roleid', roleid);
    window.location.href = 'role.html';
}

document.getElementById("addrolebutton").addEventListener("click", async () => {
    window.location.href = 'newrole.html';
});

window.onload = function () {
    loadlistrole();
}

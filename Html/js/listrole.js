async function loadlistrole() {
    let url = "https://localhost:5101/api/Role/list";

    let response = await fetch(url, {
        method: "POST",
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

function rolerow(role) {

    let tr = document.createElement("tr");
    tr.setAttribute("data-rowid", role.id);

    let nameTd = document.createElement("td");
    nameTd.append(role.name);
    tr.append(nameTd);

    /*
    let linksTd = document.createElement("td");

    const removeLink = document.createElement("button");
    removeLink.append("Удалить");
    removeLink.addEventListener("click", async () => await deleteRole(role.id));

    linksTd.append(removeLink);
    tr.appendChild(linksTd);
    */

    let linksTd = document.createElement("td");

    const editLink = document.createElement("button");
    editLink.append("Подробнее");
    editLink.className = "button";
    editLink.addEventListener("click", async () => await editrole(role.id));

    linksTd.append(editLink);
    tr.appendChild(linksTd);

    return tr;
}

function editrole(roleid) {
    localStorage.setItem('roleid', roleid);
    window.location.href = 'role.html';
}

window.onload = function () {
    loadlistrole();
}

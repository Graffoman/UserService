async function loadlistgroup() {
    let url = "https://localhost:5101/api/Group/list";

    let response = await fetch(url, {
        method: "GET",
        headers: { "Content-Type": "application/json" }
    });

    if (response.ok === true) {
        let groups = await response.json();
        let grouprows = document.getElementById("grouptbody");
        groups.forEach(group => grouprows.append(grouprow(group)));
    }
    else {
        let error = await response.json();
        console.log(error.message);
    }

}

async function deletegroup(deletegroupid) {
    let cr = confirm('Вы уверены, что хотите удалить группу?');
    if (cr) {
        let url = "https://localhost:5101/api/Group/" + deletegroupid;

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

function grouprow(group) {

    let tr = document.createElement("tr");
    tr.setAttribute("data-rowid", group.id);

    let nameTd = document.createElement("td");
    nameTd.append(group.name);
    tr.append(nameTd);

    let linksTd = document.createElement("td");

    const editLink = document.createElement("button");
    editLink.append("Подробнее");
    editLink.className = "button";
    editLink.addEventListener("click", async () => await editgroup(group.id));

    linksTd.append(editLink);
    tr.appendChild(linksTd);

    let removelinksTd = document.createElement("td");

    const removeLink = document.createElement("button");
    removeLink.className = "deletebutton";
    removeLink.append("Удалить");
    removeLink.addEventListener("click", async () => await deletegroup(group.id));

    removelinksTd.append(removeLink);
    tr.appendChild(removelinksTd); 

    return tr;
}

function editgroup(groupid) {
    localStorage.setItem('groupid', groupid);
    window.location.href = 'group.html';
}

document.getElementById("addgroupbutton").addEventListener("click", async () => {
    window.location.href = 'newgroup.html';
});

window.onload = function () {
    loadlistgroup();
}   

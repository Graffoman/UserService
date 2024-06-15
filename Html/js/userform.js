
const userid = localStorage.getItem('userid');

    async function loaduserinfo(loaduserid) {
        let url = "https://localhost:5101/api/User/" + loaduserid;

    let response = await fetch(url, {
        method: "GET",
    headers: {"Content-Type": "application/json" }
                });

if (response.ok === true) {
    let user = await response.json();
    document.getElementById("userlastname").value = user.lastName;
    document.getElementById("username").value = user.name;
    document.getElementById("usermiddlename").value = user.middleName;
    document.getElementById("useremail").value = user.email;
    document.getElementById("userbirthdaydate").value = user.birthdayDate;
    document.getElementById("userdepartment").value = user.department;
    let groups = user.groups;
    let grouprows = document.getElementById("usergrouptbody");
                    groups.forEach(group => grouprows.append(grouprow(group)));

    let roles = user.roles;
    let rolerows = document.getElementById("userroletbody");
                    roles.forEach(role => rolerows.append(rolerow(role)));

}
else {
        let error = await response.json();
        console.log(error.message);
      }

            }

    function grouprow(group) {

        let tr = document.createElement("tr");
    tr.setAttribute("data-rowid", group.id);

    let nameTd = document.createElement("td");
    nameTd.append(group.name);
    tr.append(nameTd);               

    /*
    let linksTd = document.createElement("td");

    const removeLink = document.createElement("button");
    removeLink.append("Удалить");
    removeLink.addEventListener("click", async () => await deleteGroup(group.id));

    linksTd.append(removeLink);
    tr.appendChild(linksTd);
    */

    return tr;
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

    return tr;
            }

    window.onload = function () {
                if (userid != null) {
        document.getElementById("userid").value = userid;

    loaduserinfo(userid);
                }


            }


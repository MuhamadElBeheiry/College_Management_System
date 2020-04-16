let title=document.getElementById("productName");
let credit = document.getElementById("productPrice");
let level = document.getElementById("productCompany");
let department = document.getElementById("Department");
let ProfessorInp = document.getElementById("Professor");
let addBtn = document.getElementById("addBtn");
let productsContainer =[];
let currentIndex=0;

if(localStorage.getItem("productsContainer")==null)
{
    productsContainer==null;
}
else
{
    productsContainer=JSON.parse(localStorage.getItem("productsContainer"));
    displatData();
}



addBtn.onclick = function()
{
            if(addBtn.innerHTML=="add product")
            {
                addProduct();
                clearForm();
            }
            else if(addBtn.innerHTML=="update product")
            {
                update();
                displatData();
                clearForm();
            }
}
function addProduct()
{
    let product={
            title:title.value,
            credit:credit.value,
            level:level.value,
            department:department.value,
            name:ProfessorInp.value
    };
    productsContainer.push(product);
    displatData();
}

function displatData()
{
    let col="";
    for(let i=0;i<productsContainer.length;i++)
    {
        col+=`
        <div class="our_row" style="display: table-row;">
        <div class="our-cell" style="display: table-cell;padding: 10px;border: 1px solid #ccc;">Title</div>
        <div class="our-cell" style="display: table-cell;padding: 10px;border: 1px solid #ccc;">Credit Hour</div>
        <div class="our-cell" style="display: table-cell;padding: 10px;border: 1px solid #ccc;">Level</div>
        <div class="our-cell" style="display: table-cell;padding: 10px;border: 1px solid #ccc;">Department</div>
        <div class="our-cell" style="display: table-cell;padding: 10px;border: 1px solid #ccc;">Professor</div>
        </div>
        <div class="our_row" style="display: table-row;">
        <div class="our-cell" style="display: table-cell;padding: 10px;border: 1px solid #ccc;"><p class="edit">`+productsContainer[i].title+`</p>
        </div>
        <div class="our-cell" style="display: table-cell;padding: 10px;border: 1px solid #ccc;"><p class="edit">`+productsContainer[i].credit+`</p>
        </div>
        <div class="our-cell" style="display: table-cell;padding: 10px;border: 1px solid #ccc;"><p class="edit">`+productsContainer[i].level+`</p>
        </div>
        <div class="our-cell" style="display: table-cell;padding: 10px;border: 1px solid #ccc;"><p class="edit">`+productsContainer[i].department+`</p>
        </div>
        <div class="our-cell" style="display: table-cell;padding: 10px;border: 1px solid #ccc;"><p class="edit">`+productsContainer[i].name+`</p>
        </div>

        <div class="our-cell" style="display: table-cell;padding: 10px;border: 1px solid #ccc;">
                <button class="btn btn-danger my-2" onclick="deleteProduct(${i})">delete</button>
                <button class="btn btn-info" onclick="setForm(${i})">update</button>
        </div>
    </div>`;
    }
    document.getElementById("rowData").innerHTML=col;
}
function deleteProduct(id)
{
    productsContainer.splice(id,1);
    localStorage.setItem("productsContainer",JSON.stringify(productsContainer));
    displatData();
}


function setForm(i)
{
    title.value=productsContainer[i].title;
    credit.value=productsContainer[i].credit;
    level.value=productsContainer[i].level;
    department.value=productsContainer[i].department;
    ProfessorInp.value=productsContainer[i].name;
    addBtn.innerHTML="update product";
    currentIndex=i;
}
function update()
{
    productsContainer[currentIndex].title=title.value;
    productsContainer[currentIndex].credit=credit.value;
    productsContainer[currentIndex].level=level.value;
    productsContainer[currentIndex].department=department.value;
    productsContainer[currentIndex].name=ProfessorInp.value;
    addBtn.innerHTML="add product";
    localStorage.setItem("productsContainer",JSON.stringify(productsContainer));
}

let form=document.getElementsByClassName("form-control");
function clearForm()
{
    for(let i=0;i<form.length;i++)
    {
        form[i].value="";
    }
}
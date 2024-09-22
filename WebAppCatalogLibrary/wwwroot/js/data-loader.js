let modal = $('#exampleModal');
let modalTitle = $('#modal-title');
let btnCreate = $('#btnbycreate');
let btnUpdate = $('#btnbyupdate');

let iconTrash ='<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash" viewBox="0 0 16 16"><path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0z" ></path><path d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4zM2.5 3h11V2h-11z"></path></svg> ';
let iconEdit = '<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil" viewBox="0 0 16 16"><path d="M12.146.146a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1 0 .708l-10 10a.5.5 0 0 1-.168.11l-5 2a.5.5 0 0 1-.65-.65l2-5a.5.5 0 0 1 .11-.168zM11.207 2.5 13.5 4.793 14.793 3.5 12.5 1.207zm1.586 3L10.5 3.207 4 9.707V10h.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.5h.293zm-9.761 5.175-.106.106-1.528 3.821 3.821-1.528.106-.106A.5.5 0 0 1 5 12.5V12h-.5a.5.5 0 0 1-.5-.5V11h-.5a.5.5 0 0 1-.468-.325"></path></svg> ';

$('#select-authors').select2({
    theme: "bootstrap-5",
    width: "100%",
    allowClear: true,
    multiple: true
});
$('#select-book-sections').select2({
    theme: "bootstrap-5",
    width: "100%",
    allowClear: true,
    multiple: false
});

$('form#config.tom').on('change', function (x) {
    console.log(x)
})

function hideElement(element) {
    if (!element.hasClass('visually-hidden')) {
        element.addClass('visually-hidden');
    }
}
function visibleElement(element) {
    if (element.hasClass('visually-hidden')) {
        element.removeClass('visually-hidden');
    }
}

function onClick_ModalWindow_CreateNewBook() {
    if (!modal.hasClass('d-block')) {
        modal.addClass('d-block');

        $('#select-authors').val('').change();
        hideElement(btnUpdate);
        visibleElement(btnCreate);


        let form = document.getElementById('main-form');
        form.elements['config.title'].value = '';
        form.elements['config.tom'].value = null;
        form.elements['config.location'].value = '';
        form.elements['json'].value = '';

        modalTitle.innerText = 'Новая книга';
    }
}

function onClick_ModalWindow_Close() {
    if (modal.hasClass('d-block')) {
        modal.removeClass('d-block');
    }
}

var table = new Tabulator(
    "#example-table",
    {
        data: data_table,
        columns: [
            {
                title: "Авторы",
                field: "AssociationBookAuthors.Author",
                formatter: function (cell) {
                    var data = cell.getRow().getData();
                    tmp=[]
                    var authors = data["AssociationBookAuthors"].forEach((x) => {
                        var author = x["Author"];
                        var strAuthor = [author["FirstName"], author["MiddleName"], author["SecondName"]].join(" ").trim();
                        tmp.push(strAuthor)
                    })
                    return tmp.join(", ")
                }
            },
            {
                title: "Название",
                field: "Title",
                formatter: function (cell) {
                    var data = cell.getRow().getData();
                    return data["Title"];
                },
            },
            {
                title: "Том",
                field: "Tom",
            },
            {
                title: "Раздел",
                field: "BookSection.Name",
                formatter: function (cell) {
                    var data = cell.getRow().getData();
                    return data["BookSection"]["Name"];
                },
            },
            {
                title: "Местоположение",
                field: "Location",
                formatter: function (cell) {
                    var data = cell.getRow().getData();
                    return data["Location"];
                },
            },
            {
                title: "Действия",
                field: "Action",
                formatter: function (cell, formatterParams) {
                    var data = cell.getRow().getData();

                    var container = document.createElement('div');
                    container.classList.add('d-flex');

                    //
                    // Кнопка изменение книги
                    //
                    var btnEdit = document.createElement('button');
                    btnEdit.classList.add('btn', 'btn-primary', 'me-2');
                    btnEdit.innerHTML = iconEdit;
                    btnEdit.addEventListener('click', function () {
                        if (!modal.hasClass('d-block')) {
                            modal.addClass('d-block');

                            hideElement(btnCreate);
                            visibleElement(btnUpdate);

                            modalTitle.innerText = 'Изменить книгу';
                            var json = JSON.stringify(data);

                            let form = document.getElementById('main-form');

                            form.elements['config.title'].value = data.Title;
                            form.elements['config.tom'].value = data.Tom;

                            $('#select-authors').val('').change();
                            var authors = [];
                            data.AssociationBookAuthors.forEach((x) => {
                                authors.push(x.AuthorId);
                            });
                            $('#select-authors').val(authors).change();

                            form.elements['config.section'].value = data.BookSection.BookSectionId;
                            form.elements['config.location'].value = data.Location;
                            form.elements['json'].value = json;
                        }
                    });
                    container.appendChild(btnEdit);
                    //
                    //


                    //
                    // Кнопка удаления книги
                    //
                    var btnDel = document.createElement('button');
                    btnDel.classList.add('btn', 'btn-primary');
                    btnDel.innerHTML = iconTrash;
                    btnDel.addEventListener('click', function () {
                        var json = JSON.stringify(data)
                        let form = document.getElementById('main-form');
                        form.elements['json'].value = json;
                        document.getElementById('btnbydelete').click();
                    })
                    container.appendChild(btnDel);
                    //
                    //



                    return container;
                }
            }
        ]
    });
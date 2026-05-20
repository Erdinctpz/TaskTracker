$(document).ready(function () {
    $('button[data-bs-toggle="tab"]').on('shown.bs.tab', function (e) {
        const targetId = e.target.id;
        console.log(targetId)
        if (targetId === "my-tasks-tab") {
            loadPrivateTasks();
        } else {
            //loadGrups();
        }
    });

    $('#taskModal').on('show.bs.modal', function (e) {
        const select = $('#TaskGroupId');

        $.ajax({
            url: '/Group/GetMyGroups',
            method: 'GET',
            success: function (response) {
                if (response.success) {
                    select.empty();
                    select.append('<option value="" selected>Özel Görev (Sadece Ben)</option>');

                    response.data.forEach(((taskGroup) => {
                        select.append(`<option value="${taskGroup.id}">${taskGroup.name}</option>`);
                    }))
                } else {
                    Swal.fire({
                        icon: "error",
                        title: "Hata!",
                        text: response.message,
                        confirmButtonText: "Tamam",
                    })
                }
            },
            error: function (xhr) {
                Swal.fire({
                    icon: "error",
                    title: "Hata!",
                    text: "Görev grupları getirilemedi. " + xhr.responseJSON.message,
                    confirmButtonText: "Tamam",
                })
            }
        })
    })

    $('#taskForm').on('submit', function (e) {
        e.preventDefault();

        const modal = $("#taskModal");
        const button = $('button[type="submit"]');
        button.attr('disabled', true).text("Kaydediliyor...");

        const formData = $(this).serialize();

        $.ajax({
            url: "/Task/Create",
            method: "POST",
            data: formData,
            success: function (response) {
                if (response.success) {
                    Swal.fire({
                        icon: "success",
                        title: "Başarılı!",
                        text: response.message,
                        confirmButtonText: "Tamam",
                        timer: 1500
                    });

                    loadPrivateTasks();
                } else {
                    Swal.fire({
                        title: 'Hata!',
                        text: response.message,
                        icon: 'error',
                        confirmButtonText: 'Tamam',
                    });
                }

                modal.modal('hide')
                $('#taskForm')[0].reset();
            },
            error: function (xhr) {
                Swal.fire({
                    title: 'Hata!',
                    text: `Hata Oluştu: ${xhr.responseJSON.message}`,
                    icon: 'error',
                    confirmButtonText: 'Tamam',
                });

                modal.modal('hide');
            },
            complete: function () {
                button.attr("disabled", false).text("Kaydet");
            }
        })

    });

})

function loadPrivateTasks() {
    const tableContent = $('#myTasksContent');

    $.ajax({
        url: '/Task/GetMyTasks',
        method: 'GET',
        success: function (response) {
            tableContent.html(response);
        },
        error: function () {
            Swal.fire({
                icon: "error",
                title: "Hata",
                text: "Veriler yüklenirken bir hata oluştu",
                confirmButtonText: "Tamam"
            });
        }
    })
}
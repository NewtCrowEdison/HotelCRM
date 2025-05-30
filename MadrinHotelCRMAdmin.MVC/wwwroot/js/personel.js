window.personeller = [];

window.setPersonelData = (data) => {
    window.personeller = data;
};

window.personelDuzenle = function (id) {
    const secilen = window.personeller.find(p => p.PersonelId === id);
    if (!secilen) return;

    $("input[name='PersonelId']").val(secilen.PersonelId);
    $("input[name='Ad']").val(secilen.Ad);
    $("input[name='Soyad']").val(secilen.Soyad);
    $("input[name='Email']").val(secilen.Email);
    $("input[name='Telefon']").val(secilen.Telefon);
    $("input[name='TcKimlik']").val(secilen.TcKimlik || "");
    $("input[name='PasaportNo']").val(secilen.PasaportNo || "");
    $("input[name='YabanciUyrukluMu']").prop("checked", secilen.YabanciUyrukluMu);
};

window.personelSil = function (id) {
    $.post("/AdminPanel/PersonelSil", { id })
        .done(() => location.reload())
        .fail(() => alert("Personel silinemedi."));
};

window.kayitFormGonder = function () {
    const form = new FormData($("#kayitForm")[0]);
    const id = form.get("PersonelId");

    const url = id && id !== "0"
        ? "/AdminPanel/PersonelGuncelle"
        : "/AdminPanel/PersonelKayit";

    $.ajax({
        url: url,
        type: "POST",
        data: form,
        contentType: false,
        processData: false,
        success: () => location.reload(),
        error: (xhr) => alert("Hata: " + xhr.responseText)
    });
};

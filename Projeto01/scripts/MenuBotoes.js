$(document).ready(function () {
    var caminho = window.location.pathname.split('/');
    for (var i = 0; i < caminho.length; i++) {
        if (caminho[1] == "") {
            $('#liPaginaInicial').addClass("active");
        };
        if (caminho[i] == "Fabricantes") {
            $('#liFabricantes').addClass("active");
        };
        if (caminho[i] == "Categorias") {
            $('#liCategorias').addClass("active");
        };
        if (caminho[i] == "Carrinho") {
            $('#liCarrinho').addClass("active");
        };
        if (caminho[i] == "Produtos") {
            $('#liProdutos').addClass("active");
        };
        if (caminho[i] == "Admin") {
            $('#liUsuarios').addClass("active");
        };
        if (caminho[i] == "PapelAdmin") {
            $('#liPapeis').addClass("active");
        };
    }
});

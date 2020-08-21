class ProductsView extends View {
    template(model) {
        return `
        ${model.toArray().map(product => `<div class="col-12 col-sm-6 col-lg-4 col-xl-3">
            <div class="product">
                <a href="#">
                    <img class="w-100" src="${product.img}" alt="${product.nome}">
                </a>
                <a href="#">${product.nome}</a>
                <p>${product.descricao} </p>
                <button class="w-100 text-center btn btn-primary">Comprar</button>
            </div> 
        </div>`).join('')}
        `;
    }
}

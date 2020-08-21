class ProductService {
    
    private _products = new ProductsModel()
    private _productsView = new ProductsView('#productsView')

    listarProdutos(){
        for(let i = 1; i < 10; i++)
           this._products.add(new ProductModel(i,`Product ${i}`, `descricao ${i}`, 'img/product.jpg'))

        this._productsView.update(this._products)
    }
}

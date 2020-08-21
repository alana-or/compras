abstract class View<T> {

    protected _element: Element;

    constructor(seletor: string) {

        this._element = document.querySelector(seletor);
    }

    update(model: T) {

        this._element.innerHTML = this.template(model);
    }

    abstract template(model: T): string;

}


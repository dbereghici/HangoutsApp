import * as React from 'react';
import BaseComponent from './BaseComponent/BaseComponent';
import { Header } from './Header/Header';

export default class NotFoundComponent extends BaseComponent {
    render(){
        return (
            <div>
                <Header />
                <h1> 404 Page Not Found </h1>
            </div>
        )
    }
}
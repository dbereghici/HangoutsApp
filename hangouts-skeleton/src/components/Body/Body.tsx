import * as React from 'react';

import { Route } from 'react-router-dom';
import { User } from './../User/User';

import './Body.css';

interface BodyProps {
}

interface BodyState {
}

export class Body extends React.Component<BodyProps, BodyState> {

    constructor(props: BodyProps) {
        super(props);

        this.state = {
        };
    }
    render() {
        return (
            <div className="hangouts-body">
                <Route path="/" exact={true} render={() => {
                    return <h2>Home Page</h2>;
                }} />
                <Route path="/users" component={User} />
            </div>
        );
    }
}

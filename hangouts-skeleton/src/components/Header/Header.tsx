import * as React from 'react';
import { Link } from 'react-router-dom';
import { Settings } from './../Settings/Settings';

import './Header.css';

interface HeaderProps {
}

interface HeaderState {
    nodes: Array<any>;
}

export class Header extends React.Component<HeaderProps, HeaderState> {

    constructor(props: HeaderProps) {
        super(props);

        this.state = {
            nodes: [{
                title: "Home",
                link: '/'
            }, 
            {
                title: "Groups",
                link: '/groups'
            },
             {
                title: "Users",
                link: '/users'
            }]
        };
    }

    renderNode(node: any, index: number): JSX.Element {
        return (
            <div key={index}>
                <Link to={node.link}>
                    <span>{node.title}</span>
                </Link>
            </div>
        );
    }

    render() {
        return (
            <div className="hangouts-header">
                <div className="hangouts-nodes">
                    {this.state.nodes.map(this.renderNode)}
                </div>
                <Settings />
            </div>
        );
    }
}

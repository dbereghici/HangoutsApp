import * as React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
// import { Redirect } from 'react-router';
import BaseComponent from '../BaseComponent/BaseComponent';
import { Group } from './Group';
import GroupService from '../../services/GroupService';

export class GroupList extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.nextPage = this.nextPage.bind(this);
        this.previousPage = this.previousPage.bind(this);
        this.handleSearchInput = this.handleSearchInput.bind(this);
        this.search = this.search.bind(this);
        // this.unfriend = this.unfriend.bind(this);

        this.state = {
            errorMessage: '',
            searchString: '',
            GroupData: {
                totalCount: 0,
                pageSize: 6,
                currentPage: 1,
                previousPage: "No",
                nextPage: "No",
                groups: []
            }
        }
    }

    componentDidMount() {
        GroupService.getAllGroupsPage("", this.state.GroupData.currentPage, this.state.GroupData.pageSize).then(
            (groupData) => {
                this.setState({
                    GroupData: groupData
                })
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data })
                else if (error.message)
                    this.setState({ errorMessage: error.message })
            }
        )
    }

    nextPage() {
        GroupService.getAllGroupsPage(this.state.searchString, this.state.GroupData.currentPage + 1, this.state.GroupData.pageSize).then(
            (groupData) => {
                this.setState({
                    GroupData: groupData
                })
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data })
                else if (error.message)
                    this.setState({ errorMessage: error.message })
            }
        )
    }

    previousPage() {
        GroupService.getAllGroupsPage(this.state.searchString, this.state.GroupData.currentPage - 1, this.state.GroupData.pageSize).then(
            (groupData) => {
                this.setState({
                    GroupData: groupData
                })
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data })
                else if (error.message)
                    this.setState({ errorMessage: error.message })
            }
        )
    }

    handleSearchInput(e: any) {
        const q = e.target.value;
        this.setState({
            searchString: q
        });
    }

    search(event: any){
        event.preventDefault();
        const q = event.target.value;
        this.setState({
            searchString: q
        });
        GroupService.getAllGroupsPage(this.state.searchString, this.state.GroupData.currentPage, this.state.GroupData.pageSize).then(
            (groupData) => {
                this.setState({
                    GroupData: groupData,
                    errorMessage: ''
                })
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data })
                else if (error.message)
                    this.setState({ errorMessage: error.message })
            }
        )
    }

    render() {
        let groups = this.state.GroupData.groups;
        let groups1 = [];
        let groups2 = [];
        let groups3 = [];

        for (var i = 0; i < this.state.GroupData.groups.length; i++) {
            if ((i % 3) == 0)
                groups1.push(groups[i]);
            else if ((i % 3) == 1)
                groups2.push(groups[i]);
            else
                groups3.push(groups[i]);
        }

        return (
            <div>
                <form className="demoForm"
                    onSubmit={this.search}
                >
                    <div>
                        <label htmlFor="text">Search</label>
                        <input type="text" className="form-control"
                            name="lastname" //value={this.state.lastname} //onChange={(event) => this.handleUserInput(event)} onLoad={(event) => this.handleUserInput(event)}
                            onChange={(event) => this.search(event)}
                        />
                    </div>
                    <button type="submit" className="btn btn-primary">Search</button>
                </form >
                <p> {this.state.errorMessage} </p>
                {(this.state.GroupData.groups.length === 0) ?
                    <p>
                        {/* There are no friends  */}
                    </p>
                    :
                    <div className="row">
                        <div className="col-sm-4">
                            {
                                groups1.map((group: any, i: number) =>
                                    <Group GroupData={group} key={group.id} />
                                )}
                        </div>
                        <div className="col-sm-4">
                            {
                                groups2.map((group: any, i: number) =>
                                    <Group GroupData={group} key={group.id} />
                                )}
                        </div>
                        <div className="col-sm-4">
                            {
                                groups3.map((group: any, i: number) =>
                                    <Group GroupData={group} key={group.id} />
                                )}
                        </div>
                    </div>
                }

                <button
                    className="btn btn-danger glyphicon glyphicon-danger"
                    disabled={(this.state.GroupData.previousPage === "No")}
                    onClick={this.previousPage}
                > Previous Page </button>
                <button
                    className="btn btn-danger glyphicon glyphicon-danger"
                    disabled={(this.state.GroupData.nextPage === "No")}
                    onClick={this.nextPage}
                > Next Page </button>

            </div>
        );
    }
}
import * as React from 'react';
import BaseComponent from '../BaseComponent/BaseComponent';
import { PlanService } from '../../services/PlanService';
import PlanPanel from './PlanPanel';
import AuthService from '../../services/AuthService';

export default class AllPlansOfGroupList extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.refresh = this.refresh.bind(this);
        this.nextPage = this.nextPage.bind(this);
        this.previousPage = this.previousPage.bind(this);

        this.state = {
            errorMessage: '',
            searchString: '',
            plansData: {
                totalCount: 0,
                pageSize: 6,
                currentPage: 1,
                previousPage: "No",
                nextPage: "No",
                plans: []
            },
            authUser: JSON.parse(AuthService.getUserData())
        }
    }

    componentDidMount() {
        PlanService.GetAllPlansPage(this.state.authUser.id, this.props.groupId, 1, 6).then(
            (plansData) => {
                this.setState({
                    plansData: plansData,
                    errorMessage: ''
                })
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data })
                else if (error.message)
                    this.setState({ errorMessage: error.message })
            }
        );
    }

    refresh(id: number) {
        PlanService.GetAllPlansPage(this.state.authUser.id, this.props.groupId, this.state.plansData.currentPage, this.state.plansData.pageSize).then(
            (plansData) => {
                this.setState({
                    plansData: plansData,
                    errorMessage: ''
                })
            },
            (error) => {
                this.setState({
                    plansData: { ...this.state.plansData, plans: [] },
                })
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data })
                else if (error.message)
                    this.setState({ errorMessage: error.message })
            }
        );
    }

    previousPage(){
        PlanService.GetAllPlansPage(this.state.authUser.id, this.props.groupId, this.state.plansData.currentPage - 1, this.state.plansData.pageSize).then(
            (plansData) => {
                this.setState({
                    plansData: plansData,
                    errorMessage: ''
                })
            },
            (error) => {
                this.setState({
                    plansData: { ...this.state.plansData, plans: [] },
                })
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data })
                else if (error.message)
                    this.setState({ errorMessage: error.message })
            }
        );
    }

    nextPage(){
        PlanService.GetAllPlansPage(this.state.authUser.id, this.props.groupId, this.state.plansData.currentPage + 1, this.state.plansData.pageSize).then(
            (plansData) => {
                this.setState({
                    plansData: plansData,
                    errorMessage: ''
                })
            },
            (error) => {
                this.setState({
                    plansData: { ...this.state.plansData, plans: [] },
                })
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data })
                else if (error.message)
                    this.setState({ errorMessage: error.message })
            }
        );
    }

    render() {
        let plans = this.state.plansData.plans;
        let plans1 = [];
        let plans2 = [];
        let plans3 = [];

        for (var i = 0; i < this.state.plansData.plans.length; i++) {
            if ((i % 3) == 0)
                plans1.push(plans[i]);
            else if ((i % 3) == 1)
                plans2.push(plans[i]);
            else
                plans3.push(plans[i]);
        }

        return (
            <div>
                <h1> Plans </h1>                                
                <h3> {this.state.errorMessage} </h3>
                {(this.state.plansData.plans.length === 0) ?
                    <p>
                    </p>
                    :
                    <div>
                        <div className="row">
                            <div className="col-sm-4">
                                {
                                    plans1.map((plan: any, i: number) =>
                                        <PlanPanel plan={plan} refresh={this.refresh} hideButton={false} />
                                    )}
                            </div>
                            <div className="col-sm-4">
                                {
                                    plans2.map((plan: any, i: number) =>
                                        <PlanPanel plan={plan} refresh={this.refresh} hideButton={false} />
                                    )}
                            </div>
                            <div className="col-sm-4">
                                {
                                    plans3.map((plan: any, i: number) =>
                                        <PlanPanel plan={plan} refresh={this.refresh} hideButton={false} />
                                    )}
                            </div>
                        </div>

                        <button
                            className="btn btn-danger glyphicon glyphicon-danger"
                            disabled={(this.state.plansData.previousPage === "No")}
                            onClick={this.previousPage}
                        > Previous Page </button>
                        <button
                            className="btn btn-danger glyphicon glyphicon-danger"
                            disabled={(this.state.plansData.nextPage === "No")}
                            onClick={this.nextPage}
                        > Next Page </button>
                    </div>
                }
            </div>
        )
    }
}
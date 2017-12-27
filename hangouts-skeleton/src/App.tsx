import * as React from 'react';
import { BrowserRouter as Router } from 'react-router-dom';

import './App.css';
import 'font-awesome/css/font-awesome.css';

// import { Header } from './../src/components/Header/Header';
// import { Body } from './../src/components/Body/Body';
import Home from './components/Home/Home'
import { Route } from 'react-router';
import { Authentication } from './components/Authentication/Authentication';
import { MyProfile } from './components/MyProfile/MyProfile';
import { Friends } from './components/Friends/Friends';
import { Groups } from './components/Group/Groups';


class App extends React.Component {
  render() {
    return (
      <Router>
        <div className="app">
          {/* <Header />
          <Body /> */}

          <Route path="/" exact={true} component={Authentication} />
          <Route path="/home" component={Home} />
          <Route path="/authentication" component={Authentication} />
          <Route path="/myprofile" component={MyProfile}/>
          <Route path="/friends" component={Friends}/>
          <Route path="/groups" component={Groups}/>
        </div>
      </Router>
    );
  }
}

export default App;

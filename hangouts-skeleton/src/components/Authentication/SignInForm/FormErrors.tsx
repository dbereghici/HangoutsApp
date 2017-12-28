import * as React from 'react';

export default class FormErrors extends React.Component<any, any> {
    constructor(props : any){
        super(props);
    }


    render() {
        return (
            <div className='formErrors'>
            {Object.keys(this.props.formErrors).map((fieldName, i) => {
              if(this.props.formErrors[fieldName].length > 0){
                return (
                  <p key={i}>{fieldName} {this.props.formErrors[fieldName]}</p>
                )        
              } else {
                return '';
              }
            })}
          </div>
        );
    }
}

import React, {  Component } from 'react';
import { List, Datagrid, TextField, useRecordContext, AutocompleteInput } from 'react-admin';

const LightTextField = ({ source }) => {

    const record = useRecordContext();

    let color = 'none';

    if (record.light === 1) { color = '#90ee90'; }
    if (record.light === 2) { color = '#00FF00'; }
    if (record.light === 3) { color = 'yellow'; }
    if (record.light === 4) { color = 'red'; }

    record.light = " ";

    return (<div style={{ backgroundColor: color, width: '100%', height: '10px', border: '1px solid black' }}>{record && record[source]}</div>);

};

const PassportFormRender = choice => {

    if (choice.record.designation === undefined || choice.record.name === undefined) { return " "; }

    return (

        <div style={{ wordWrap: 'break-word', width: '400px' }}>
            <div style={{ whiteSpace: 'normal' }}>[{choice.record.designation}]</div>
            <div style={{ whiteSpace: 'normal' }}>{choice.record.name}</div>          
        </div>
        
     );
    
};

const inputText = choice => `[${choice.designation == null ? " " : choice.designation}] ${choice.name}`;

export class SchedulerList extends Component {

    constructor(props) {

        super(props);


        this.state = {
            /*
            *
            * Все переменные
            *
            */
            url: 'https://localhost:44307/api/'

        };

    }

    //Получить данные через API для фильтра
    getData(container, controller, filter) {

        let request = this.state.url + controller;

        if (filter !== null || filter !== undefined) {

            request += "?";
            request += filter;

        }

        fetch(request)
            .then(resp => resp.json())
            .then(

                (result) => {

                    this.setState({

                        [container]: JSON.parse(result)

                    });

                }

            );

    }

    //Заполнить все фильтры
    fillFilters() {

        this.getData('subjjurList', 'data/subjjur');
        this.getData('pfvChapterList', 'data/passportform', 'levelnum=1');
        this.getData('pfvDesignationList', 'data/passportform', 'levelnum=2');

    }

    componentDidMount() {

        this.fillFilters();

    }

    render() {

        //choices должны соответствовать наименованию контейнера в this.getData()
        let schedulerFilters = [

            <AutocompleteInput
                source="subj_jur_id"
                label="Предприятие"
                choices={this.state.subjjurList}
                optionText="name_short"
                optionValue="id"
                alwaysOn />,

            <AutocompleteInput
                source="hdpp_passport_form_ver_id"
                label="Раздел"
                choices={this.state.pfvChapterList}
                optionValue="id"

                optionText={<PassportFormRender />}
                inputText={inputText}
                matchSuggestion={(filterValue, suggestion) => true}

                //Изменяемый (зависимый контейнер), контроллер, зависимое поле + выбранное значение
                onChange={event => this.getData('pfvDesignationList', 'data/passportform', 'hdpp_passport_form_ver_id=' + event)}
                alwaysOn />,

            <AutocompleteInput
                source="name"
                label="Код формы"
                choices={this.state.pfvDesignationList}

                optionText={<PassportFormRender />}
                inputText={inputText}
                matchSuggestion={(filterValue, suggestion) => true}

                optionValue="id"
                alwaysOn />,



            //<DateInput
            //    source="dt_form"
            //    label="Срок предоставления с:"
            //    alwaysOn />

        ];

        return (

            <List title="Графики сдачи форм" {...(this.props)} filters={schedulerFilters}>

                <Datagrid>
                    <TextField source="sj_name" label="Предприятие" />
                    <TextField source="s_designation" label="Код формы" />
                    <TextField source="s_name" label="Наименование формы" />
                    <TextField source="period_name" label="Периодичность ФП" />
                    <TextField source="year_form" label="Год" />
                    <TextField source="pd_name" label="Отчетный период" />
                    <TextField source="dt_form" label="Срок представления" />
                    <TextField source="status" label="Статус" />
                    <LightTextField source="light" label="Светофор" />
                </Datagrid>

            </List>

        );

    }

};

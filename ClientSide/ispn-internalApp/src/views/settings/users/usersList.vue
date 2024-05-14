<template>
  <v-container
    id="usersList"
    fluid
    tag="section"
    class="full-height"
    v-if="isAdministrator"
  >
    <base-v-component
      heading="Потребители"
    />

    <v-row class="my-5 d-print-none">
      <v-col cols="12" class="text-right">
        <v-btn
          class="primary px-5"
          @click="newUser"
        >
          <v-icon left dark>mdi-plus</v-icon>
          Нов потребител
        </v-btn>
      </v-col>
    </v-row>

    <users-filter
      ref="filterUsers"
      @doFilter="getData"
    />

    <base-material-card
      color="primary"
      icon="mdi-gesture-double-tap"
      inline
      class="px-5 py-3 mt-15"
    >
    
      <template v-slot:after-heading>
        <div class="display-2 font-weight-light">Потребители</div>
      </template>

      <base-kendo-grid 
        :columns="table.headers"
        :items="table.data"
        :pagination="table.pagination"
        @getData="getData"
        @click="tableActions"
        @dblclick="preview"
      />
    </base-material-card>
  </v-container>
</template>

<script>
import { mapGetters } from "vuex";
import { UsersFilter } from '@/components';
//import { apiGetUsers } from '@/api/task-shreder/account';
import { isEmptyObject } from '@/utils';

export default {
  name: 'UsersList',
  components: {
    UsersFilter,
  },
  data: () => ({
    table: {
      headers: [],
      data: [],
      pagination: {
        skip: 0,
        take: 20,
        perPageOptions: [5, 10, 20, 50, 100],
        total: 0
      }
    }
  }),
  computed: {
    ...mapGetters([
      'isAdministrator'
    ]),
  },
  mounted(){
    this.table.headers = [
      { title: 'Име', field: 'person.fullName'},
      { title: 'Потребителско име', field: 'userName' },
      { title: 'E-mail', field: 'person.email' },
      { title: 'Роли', field: 'roles', cell: this.getUserRoles},
      { title: 'Статус', field: 'isActive', cell: 'status' },
      { title: "", cell: "actions", filterable: false, width: '50px', buttons: [
        { title: 'Преглед', icon: 'mdi-text-box-search-outline', color: 'white', class: 'primary', click: 'preview' }
      ]},
    ]
    this.getData();
  },
  methods: {
    getData(){
      let body = Object.assign({}, this.$refs.filterUsers.filters);

      if(isEmptyObject(body))
        body = {
          Number: ''
        }

      if(body.page === 1){
        delete body.page;
        this.table.pagination.skip = 0
      }

      let params = {
        skip: this.table.pagination.skip,
        take: this.table.pagination.take
      }

      // apiGetUsers(params, body).then(result => {
      //   this.table.data = result.items;
      //   this.table.pagination.total = result.totalCount;
      // })
    },
    newUser(){
      this.$router.push({path: '/settings/users/create'})
    },
    preview(user){
      this.$router.push({path: `/settings/users/${user.id}`})
    },
    getUserRowRoles(user){
      let result = '';
      if(user && user.roles){
        for(let role of user.roles){
          if(result.length){
            result += `, ${role.name}`
          } else {
            result = role.name;
          }
        }
      }

      return result;
    },
    getUserRoles(h, tdElement , props ) {
      return h('td', null, [
        this.getUserRowRoles(props.dataItem)
      ]);
    },
    tableActions({action, rowData}){
      switch(action){
        case "preview":
          this.preview(rowData)
        break;
      }
    },
  }
}
</script>

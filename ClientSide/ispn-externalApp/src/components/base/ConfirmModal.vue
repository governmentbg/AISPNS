<template>
  <v-row justify="center">
    <v-dialog
      v-model="open"
      persistent
      max-width="600"
    >
      <v-card>
        <v-card-title class="headline">
          {{title}}
        </v-card-title>
        <v-card-text>
          <div v-html="body" />
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            :color="$vuetify.theme.isDark ? '' : 'primary darken-1'"
            @click="confirm"
          >
            Продължи
          </v-btn>
          <v-btn
            :color="$vuetify.theme.isDark ? 'grey darken-2' : 'default darken-3'"
            @click="open = false"
          >
            Откажи
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script>
  export default {
    name: 'ConfirmActionModal',
    props: {},
    data (){
      return {
        open: false,
        title: null,
        body: null,
        parameter: null
      }
    },
    methods: {
      confirm(){
        if(this.parameter){
          this.callback(this.parameter);
        } else {
          this.callback()
        }
      },
      openModal(data){
        this.title = data.title;
        this.body = data.body;
        this.parameter = data.parameter;
        if(data.callback)
          this.callback = data.callback;
        
        this.open = true;
      },
      closeModal(){
        this.open = false;
      }
    }
  }
</script>
<template>
  <base-material-card 
    icon="mdi-gavel"
    color="primary"
    inline
  >
    <template v-slot:after-heading>
      <div>Дело</div>
      <small>{{ isBankruptcy ? 'по производство по несъстоятелност' : 'по производство по стабилизация' }}</small>
    </template>

    <v-container class="py-7">
      <v-simple-table class="table-presentation">
        <template v-slot:default>
          <tbody>
            <tr>
              <td>Съд:</td>
              <td>{{ data.court?.name }}</td>
            </tr>
            <tr>
              <td>Номер:</td>
              <td>{{ caseNumber }}</td>
            </tr>
            <tr>
              <td>Дата:</td>
              <td>{{ data.formationDate ? ISODateString(data.formationDate) : '' }}</td>
            </tr>
            <tr>
              <td>Вид:</td>
              <td>{{ data.caseKind?.description }}</td>
            </tr>
          </tbody>
        </template>
      </v-simple-table>
    </v-container>
  </base-material-card>
</template>

<script>
import { ISODateString } from "@/utils";

export default {
  name: "caseCardData",
  props: {
    data: {
      type: Object,
      required: true,
      default: () => ({
        court: {},
        kind: {},
        formationDate: null
      })
    },
    isBankruptcy: {
      type: Boolean,
      default: true
    }
  },
  data(){
    return {

    }
  },
  computed: {
    caseNumber(){
      if(this.data && this.data.number && this.data.year)
        return `${this.data.number}/${this.data.year}`;

      return '';
    },
  },
  methods: {
    ISODateString
  }
}
</script>
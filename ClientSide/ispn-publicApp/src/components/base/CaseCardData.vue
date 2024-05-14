<template>
  <base-material-card 
    icon="mdi-gavel"
    color="primary"
    inline
  >
    <template v-slot:after-heading>
      <div>{{ $t('_case')}}</div>
      <small>{{ isBankruptcy ? $t('in_bankruptcy_proceedings') : $t('in_stabilization_proceedings') }}</small>
    </template>

    <v-container class="py-7">
      <v-simple-table class="table-presentation">
        <template v-slot:default>
          <tbody>
            <tr>
              <td>{{ $t('court') }}:</td>
              <td>{{ data.courtName }}</td>
            </tr>
            <tr>
              <td>{{ $t('number') }}:</td>
              <td>{{ caseNumber }}</td>
            </tr>
            <tr>
              <td>{{ $t('date') }}:</td>
              <td>{{ data.formationDate ? ISODateString(data.formationDate) : '' }}</td>
            </tr>
            <tr>
              <td>{{ $t('kind') }}:</td>
              <td>{{ data.caseKindDescription }}</td>
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
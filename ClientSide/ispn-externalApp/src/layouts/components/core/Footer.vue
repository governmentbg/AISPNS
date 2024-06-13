<template>
  <v-footer
    id="dashboard-core-footer"
    class="white mt-15"
    height="100px"
    absolute
    inset
  >
    <v-row id="euFundBanner" class="mb-0" align="center" v-if="false">
      <v-col cols="12" sm="3" md="2" lg="2" xl="2"  class="pl-5">
        <v-img
          :src="ProgFlag1"
          class="prog-img"
        />
      </v-col>
      <v-col cols="12" sm="6" md="8" lg="8" xl="8" align="center">
        Проект: BG16RFOP002-2.064 – 0001-С01 "Предоставяне на подобрени услуги и осигуряване на благоприятна бизнес среда посредством внедряване на Единна система за управление, контрол и анализ на дейностите на Българския институт по метрология" се осъществява с финансовата подкрепа на Оперативна програма „Иновации и конкурентоспособност“ 2014-2020, съфинансирана от Европейския съюз чрез Европейския фонд за регионално развитие. www.eufunds.bg.
      </v-col>
      <v-col cols="12" sm="3" md="2" lg="2" xl="2" class="pr-5">
        <v-img
          :src="ProgFlag2"
          class="prog-img"
        />
      </v-col>
    </v-row>
  </v-footer>
</template>

<script>
import config from "@/config";
import ProgFlag1 from "@/assets/prog-flag.png";
import ProgFlag2 from "@/assets/prog-flag2.png";
export default {
  name: 'CoreAppFooter',

  data: () => ({
    links: [],
    title: config.title,
    ProgFlag1: ProgFlag1,
    ProgFlag2: ProgFlag2
  }),
  computed: {
    year(){
      let year = (new Date).getFullYear();
      
      if(year <= 1997) return "2022"

      return "1997 - "+year;
    }
  }
}
</script>

<style lang="sass">
#dashboard-core-footer
  #euFundBanner
    font-size: 0.85rem
</style>

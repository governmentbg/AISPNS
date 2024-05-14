<template>
  <Editor
    v-model="internalValue"
    api-key="kqmibweb22bv4el1bowez9qs2iu3ua9svnhp5hqcbe617ric"
    :init="init"
  />
</template>



<script>

import Editor from '@tinymce/tinymce-vue';
import Proxyable from 'vuetify/lib/mixins/proxyable';
export default {
  name: 'base-editor',
  components: {
    Editor
  },
  data(){
    return {
      init: {
        language: 'bg_BG',
        menubar: false,
        min_height: 500,
        plugins: [
          'autoresize advlist autolink lists link image charmap print preview anchor',
          'searchreplace visualblocks code fullscreen',
          'insertdatetime media table paste code help wordcount table'
        ],
        toolbar:
          'undo redo | formatselect | bold italic backcolor | \
          table | \
          alignleft aligncenter alignright alignjustify | \
          link image | \
          bullist numlist outdent indent | removeformat | code | help',
          paste_preprocess: function(editor, args) {
            args.content = args.content.replace(/(<table[^\>]*)/gi, '<table class="wytiwyg-table"')
          },
          content_style: `table.wytiwyg-table { border: 1px solid #9d9d9d !important; border-collapse: collapse !important; width: 100%; margin-bottom: 20px; } table.wytiwyg-table tr td { padding: 5px; border: 1px solid #9d9d9d !important; } table.wytiwyg-table tr td p { margin: 0; }`,
      }
    }
  },
  mixins: [Proxyable],
}
</script>

